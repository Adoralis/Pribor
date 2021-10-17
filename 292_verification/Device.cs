using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace _292_verification
{
    public class Device 
    {
        public static byte[] RecievedData;
        public static SerialPort Port;

        /// <summary>
        /// Функция расчета CRC16 
        /// </summary>
        /// <param name="msg">Код, для которого считается CRC</param>
        /// <param name="length">Количества байт кода</param>
        /// <returns></returns>
        public static ushort GetCRC16(byte[] msg, ushort length)            
        {
            const ushort basis = 0xA001;                //основание для расчета CRC 
            ushort code = 0xFFFF;

            for (int i = 0; i < length; i++)
            {
                //Для каждого байта массива
                byte FirstByte = (byte)(code);          //в FirstByte помещаем младший байт 16-ти разрядного числа code (будущий СRС16)
                FirstByte ^= msg[i];                    //производим XOR между FirstByte и msg[i] и помещаем его в FirstByte
                code &= 0xFF00;                         //обнуляем младший байт числа code
                code += FirstByte;
                for (int j = 0; j < 8; j++)
                {
                    if ((code & 0x0001) == 1)           //выделяем младший бит code и проверяем его на 1 или 0
                    {//Младший бит code = 1
                        code >>= 1;                     //сдвиг вправо на 1 бит с присвоением
                        code ^= basis;                //операция XOR переменных code с polinom с помещением результата в code
                    }
                    else
                    {//Младший бит code = 0
                        code >>= 1;                     //сдвиг вправо на 1 бит с присвоением
                    }
                }
            }
            return code;
        }//end GetCRC16

        /// <summary>
        /// Функция обработки входящих данных
        /// </summary>
        /// <param name="data">Данные</param>
        /// <param name="StartByte">Стартовый байт для определения начала посылки</param>
        /// <param name="NumByte">количество байт в посылке</param>
        /// <returns></returns>
        public static bool DataRecieve(byte[] data, byte StartByte, byte NumByte)
        {
            byte[] newdata;
            ushort code_CRC16_new;
            int NumRecievedBytes = 0;

            //Ищем начало массива
            for (ushort i = 0; i < data.Length; i++)
            {
                if (data[i] == StartByte)                               //Нашли начало посылки
                {
                    newdata = new byte[NumByte];                        //создает новый массив из нужного кол-ва элементо куда переписывает посылку

                    for (ushort j = 0; j < (data.Length - i); j++)      //Обрабатываем посылку, (data.Length - i) - потому что не учитываем стартовый байт
                    {
                        NumRecievedBytes++;
                        newdata[j] = data[i + j];                       //Записываем истинные байты

                        if (NumRecievedBytes == NumByte)
                        {//Кол-во принятых байт сравнялось с необходимым
                            //Обнуление счетчика принятых байт
                            NumRecievedBytes = 0;
                            //Проверка CRC
                            //Считаем CRC16 полученного массива
                            code_CRC16_new = Device.GetCRC16(newdata, (ushort)(newdata.Length - 2));    // Подсчет контрольной суммы нового массива

                            byte low_CRC16;                                                                                             //младший байт CRC16 полученного массива
                            byte high_CRC16;                                                                                            //старший байт CRC16 полученного массива

                            //Вычленяем CRC16 из полученного массива
                            low_CRC16 = (byte)(code_CRC16_new);                                                                         //Расчет первого байта CRC
                            high_CRC16 = (byte)((code_CRC16_new & 0xff00) >> 8);                                                        //Расчет второго байта CRC
                            //Если CRC не сошлось, то ошибка и выставление флага о неудачном приеме
                            //Сравниваем рассчитанную сумму с полученной
                            if ((newdata[(newdata.Length - 2)] != low_CRC16) && (newdata[(newdata.Length - 1)] != high_CRC16))          //Проверка контрольной суммы
                            {
                                return false;
                            }
                            //CRC сошлось - расшифровка массива, выставление флага об удачном приеме ответа
                            else
                            {
                                RecievedData = newdata;
                                return true;        //завершение подпрограммы
                            }
                        }

                    }
                }

            }//end for()
            return false;
        }//end datarecieve

 
        /// <summary>
        /// Функция "упаковки" сообщения для отправки по RS-232
        /// Рассчитывает CRC для принятых данных
        /// </summary>
        /// <param name="data">содержание посылки</param>
        /// <returns></returns>
        private static byte[] GetMessage(byte[] data)
        {
            ushort code_CRC16;

            code_CRC16 = Device.GetCRC16(data, (ushort)data.Length);                //расчет CRC на входные данные
            byte crc_l = (byte)(code_CRC16);
            code_CRC16 &= 0xff00;                                                   //обнуляем младший байт числа code
            byte crc_h = (byte)(code_CRC16 >> 8);
            byte[] msg = new byte[data.Length + 2];                                 // {data[0],data[1],data[2],  m2, m3 };
            //формируем посылку
            for (int i = 0; i < data.Length; i++)
                msg[i] = data[i];
            msg[data.Length] = crc_l;
            msg[data.Length + 1] = crc_h;

            return msg;
        }


        /// <summary>
        /// Функция для отправки двух байт
        /// </summary>
        /// <param name="data0">номер массива</param>
        /// <param name="data1">данные</param>
        /// <returns></returns>
        public static byte[] TransmitMessage(byte data0, byte data1)
        {
            //формирование сообщения для отправки
            byte[] command = new byte[] { data0, data1 };//0-номер массива, 1-данные
            byte[] message = GetMessage(command);

            return message;
        }

        /// <summary>
        /// функция для отправки одного байта
        /// </summary>
        /// <param name="data0">нмоер массива</param>
        /// <returns></returns>
        public static byte[] TransmitMessage(byte data0)
        {
            //формирование сообщения для отправки
            byte[] command = new byte[] { data0 };//0-номер массива
            byte[] message = GetMessage(command);

            return message;
        }



        /// <summary>
        /// Функция определения доступного COM порта
        /// </summary>
        /// <returns></returns>
        public static bool  Port_finder()
        {
            string[] ports = SerialPort.GetPortNames();
            byte[] sign = TransmitMessage(0xFE);                                        //признак КПрА
            bool exit = false;

            foreach (string port in ports)
            {                            
                SerialPort sp = new SerialPort();
                sp.PortName = port;                                 
                sp.Open();
                if (sp.IsOpen)
                {
                    sp.Write(sign, 0, sign.Length);

                    if (sp.BytesToRead != 0)
                    {
                        byte[] answer = new byte[sp.BytesToRead];
                        sp.Read(answer, 0, answer.Length);
                        if (sign == answer)
                        {
                            Port = sp;
                            return exit = true;
                        }
                    }
                }
                if (!exit)
                    sp.Close();
            }
            return exit = false;
        }
    }
}
