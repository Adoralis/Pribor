using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace _292_verification
{
    public class Device : Form
    {
        public static byte[] RecievedData;

        public static ushort GetCRC16(byte[] msg, ushort length)
        {
            const ushort polinom = 0xa001;
            ushort code = 0xffff;

            for (int i = 0; i < length; i++)
            {
                //Для каждого байта массива
                byte FirstByte;
                FirstByte = (byte)(code);          //в ml помещаем младший байт 16-ти разрядного числа code (будущий СRС16)
                FirstByte ^= msg[i];               //производим XOR между ml и msg[i] и помещаем его в ml
                code &= 0xff00;             //обнуляем младший байт числа code
                code += FirstByte;
                for (int j = 0; j < 8; j++)
                {
                    //выделяем младший бит code и проверяем его на 1 или 0
                    if ((code & 0x0001) == 1)
                    {//Младший бит code = 1
                        code >>= 1;         //сдвиг вправо на 1 бит с присвоением
                        code ^= polinom;    //операция XOR переменных code с polinom с помещением результата в code
                    }
                    else
                    {//Младший бит code = 0
                        code >>= 1;         //сдвиг вправо на 1 бит с присвоением
                    }
                }
            }
            return code;
        }//end GetCRC16

        public static bool DataRecieve(byte[] data, byte StartByte, byte NumByte)
        {
            byte[] newdata;
            ushort code_CRC16_new;
            int NumRecievedBytes = 0;

            //Ищем начало массива
            for (ushort i = 0; i < data.Length; i++)
            {
                if (data[i] == StartByte)
                {
                    newdata = new byte[NumByte];    //создает новый массив из нужного кол-ва элементо куда переписывает найденные значения

                    for (ushort j = 0; j < (data.Length - i); j++)
                    {
                        NumRecievedBytes++;
                        newdata[j] = data[i + j];

                        if (NumRecievedBytes == NumByte)
                        {//Кол-во принятых байт сравнялось с необходимым
                            //Обнуление счетчика принятых байт
                            NumRecievedBytes = 0;
                            //Проверка CRC
                            //Считаем CRC16 полученного массива
                            code_CRC16_new = Device.GetCRC16(newdata, (ushort)(newdata.Length - 2)); // Подсчет контрольной суммы нового массива

                            byte low_CRC16;                                                        //младший байт CRC16 полученного массива
                            byte high_CRC16;                                                       //старший байт CRC16 полученного массива

                            //Вычленяем CRC16 из полученного массива
                            low_CRC16 = (byte)(code_CRC16_new);
                            high_CRC16 = (byte)((code_CRC16_new & 0xff00) >> 8);
                            //Если CRC не сошлось, то ошибка и выставление флага о неудачном приеме
                            //Сравниваем рассчитанную сумму с полученной
                            if (newdata[(newdata.Length - 2)] != low_CRC16 && newdata[(newdata.Length - 1)] != high_CRC16)           //Проверка контрольной суммы
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

        //Подпрограмма  GetMessage
        //формирует сообщение для отправки по RS-232 согласно протоколу
        private static byte[] GetMessage(byte[] data)
        {
            ushort code_CRC16;

            code_CRC16 = Device.GetCRC16(data, (ushort)data.Length);
            byte crc_l;
            byte crc_h;
            crc_l = (byte)(code_CRC16);
            code_CRC16 &= 0xff00;             //обнуляем младший байт числа code
            crc_h = (byte)(code_CRC16 >> 8);
            byte[] msg = new byte[data.Length + 2];// {data[0],data[1],data[2],  m2, m3 };
            //формируем посылку
            for (int i = 0; i < data.Length; i++)
                msg[i] = data[i];
            msg[data.Length] = crc_l;
            msg[data.Length + 1] = crc_h;

            return msg;
        }

        public static byte[] TransmitMessage(byte data0, byte data1)
        {
            //формирование сообщения для отправки
            byte[] command = new byte[] { data0, data1 };//0-номер массива, 1-данные
            byte[] message = GetMessage(command);

            return message;
        }
        public static byte[] TransmitMessage(byte data0)
        {
            //формирование сообщения для отправки
            byte[] command = new byte[] { data0 };//0-номер массива
            byte[] message = GetMessage(command);

            return message;
        }

        public int Port_finder()
        {
            SerialPort sp = new SerialPort();
            for (int i = 0; i < 9; i++)
            {
                sp.PortName = $"COM{i}";
                sp.Open();
                if (sp.IsOpen)
                {
                    return 1;
                }
                if (i == 9)
                {
                    MessageBox.Show("Неудается открыть COM порт", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);//Выдается ошибка
                    return 0;
                }
            }
        }
    }
}
