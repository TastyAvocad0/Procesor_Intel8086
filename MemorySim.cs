using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using Registry = System.Collections.Generic.Dictionary<string, byte>;


public static class MemorySim

{
    static Registry registers = new Registry {
        {"AH", 0 },
        {"AL", 0 },
        {"BH", 0 },
        {"BL", 0 },
        {"CH", 0 },
        {"CL", 0 },
        {"DH", 0 },
        {"DL", 0 }
    };

    static HashSet<Address> UsedAdresses = new HashSet<Address>();
    static byte[] memory = new byte[1 << 16];

    private class Address {
        private bool isReg;
        private ushort address;
        private string register;

        public Address(string reg) {
            register = reg;
            isReg = true;
        }

        public Address(ushort addr) {
            address = addr;
            isReg = false;
        }


        public void SetValue(byte v) {
            if(isReg)
                registers[register] = v;
            else
                memory[address] = v;
        }

        public byte GetValue() {
            if(isReg)
                return registers[register];
            return memory[address];
        }

        public void Print() {
            if(isReg) 
                return;

            Console.WriteLine(address.ToString("X4") + " : " + GetValue().Str());
        }

        public override bool Equals(object other) {
            if(other is Address addr)
               return isReg == addr.isReg && (address == addr.address || register == addr.register);
            return false;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(register, address);
        }
    }

    public static void Run() 
    {
        Console.WriteLine("Wprowadz wartosci rejestrow");

        foreach (string key in new List<string>(registers.Keys))
        {
            byte register = 0;
            ReadValue(key + ": ", "Wpisz poprawny znak", str => TryToNumber(str, out register));
            registers[key] = register;
        }

        while (true)
            {
                Console.WriteLine("\n=============================");
                Console.WriteLine("1) Mov");
                Console.WriteLine("2) Xchg");
                Console.WriteLine("3) Not");
                Console.WriteLine("4) Inc");
                Console.WriteLine("5) Dec");
                Console.WriteLine("6) And");
                Console.WriteLine("7) Or");
                Console.WriteLine("8) Xor");
                Console.WriteLine("9) Add");
                Console.WriteLine("10) Sub");
                Console.WriteLine("11) Print Registers");
                Console.WriteLine("x) exit");
                Console.WriteLine("=============================");

                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                    {
                        Address srcAdr, dstAdr;

                        ReadAddress("Podaj adres docelowy: ", "Zly adres", out srcAdr);

                        ReadAddress("Podaj adres Ÿród³owy: ", "Zly adres", out dstAdr);

                        if (srcAdr == dstAdr)
                        {
                            Console.WriteLine("NOP");
                            break;
                        }

                        dstAdr.SetValue(srcAdr.GetValue());
                        UsedAdresses.Add(dstAdr);
                        UsedAdresses.Add(srcAdr);
                        break;
                    }

                    case 2:
                    {
                        Address Mem1, Mem2;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem1);

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem2);

                        if (Mem1 == Mem2)
                        {
                            Console.WriteLine("NOP");
                            break;
                        }

                        byte tmp = Mem1.GetValue();
                        Mem1.SetValue(Mem2.GetValue());
                        Mem2.SetValue(tmp);
                        UsedAdresses.Add(Mem1);
                        UsedAdresses.Add(Mem2);
                        break;
                    }

                    case 3:
                    {
                        Address Mem;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem);
                        Mem.SetValue((byte)~Mem.GetValue());
                        UsedAdresses.Add(Mem);
                        break;
                    }

                    case 4:
                    {
                        Address Mem;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem);
                        
                        Mem.SetValue((byte)(Mem.GetValue() + 1));
                        UsedAdresses.Add(Mem);
                        break;
                    }

                    case 5:
                    {
                        Address Mem;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem);
                        Mem.SetValue((byte)(Mem.GetValue() - 1));
                        UsedAdresses.Add(Mem);
                        break;
                    }

                    case 6:
                    {
                        Address Mem1, Mem2;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem1);

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem2);

                        if (Mem1 == Mem2 )
                        {
                            Console.WriteLine("NOP");
                            break;
                        }

                        Mem1.SetValue((byte)(Mem1.GetValue() & Mem2.GetValue()));
                        UsedAdresses.Add(Mem1);
                        UsedAdresses.Add(Mem2);
                        break;
                    }

                    case 7:
                    {
                        Address Mem1, Mem2;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem1);

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem2);

                        if (Mem1 == Mem2)
                        {
                            Console.WriteLine("NOP");
                            break;
                        }

                        Mem1.SetValue((byte)(Mem1.GetValue() | Mem2.GetValue()));
                        UsedAdresses.Add(Mem1);
                        UsedAdresses.Add(Mem2);
                        break;
                    }

                    case 8:
                    {
                        Address Mem1, Mem2;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem1);

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem2);

                        if (Mem1 == Mem2)
                        {
                            Console.WriteLine("NOP");
                            break;
                        }

                        Mem1.SetValue((byte)(Mem1.GetValue() ^ Mem2.GetValue()));
                        UsedAdresses.Add(Mem1);
                        UsedAdresses.Add(Mem2);
                        break;
                    }

                    case 9:
                    {
                        Address Mem1, Mem2;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem1);

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem2);

                        if (Mem1 == Mem2)
                        {
                            Console.WriteLine("NOP");
                            break;
                        }

                        Mem1.SetValue((byte)(Mem1.GetValue() + Mem2.GetValue()));
                        UsedAdresses.Add(Mem1);
                        UsedAdresses.Add(Mem2);
                        break;
                    }

                    case 10:
                    {
                        Address Mem1, Mem2;

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem1);

                        ReadAddress("Podaj adres: ", "Zly adres", out Mem2);

                        if (Mem1 == Mem2)
                        {
                            Console.WriteLine("NOP");
                            break;
                        }

                        Mem1.SetValue((byte)(Mem1.GetValue() - Mem2.GetValue()));
                        UsedAdresses.Add(Mem1);
                        UsedAdresses.Add(Mem2);
                        break;
                    }


                    case 11:
                        PrintMemory();
                        break;

                    default:
                        return;
                }
            }
        
    }

    static bool TryToNumber(string s, out byte result) 
    {
        if(s.Length != 2)
        {
            result = 0;
            return false;   
        }

        s = s.ToLower();

        static int Hex2Int(char c) 
        {
            if(c >= '0' && c <= '9')
                return c - '0';

            if(c >= 'a' && c <= 'f')
                return c - 'a' + 10;

            return -1;
        }

        int x0 = Hex2Int(s[0]);
        int x1 = Hex2Int(s[1]);
        
        if(x0 < 0 || x1 < 0)
        {
            result = 0;
            return false;
        }

        result = (byte)(x0 * 16 + x1);
        return true;
    }
    
    static bool TryToAddress(string s, out ushort result) 
    {
        if(s.Length != 4)
        {
            result = 0;
            return false;   
        }

        s = s.ToLower();

        static int Hex2Int(char c) 
        {
            if(c >= '0' && c <= '9')
                return c - '0';

            if(c >= 'a' && c <= 'f')
                return c - 'a' + 10;

            return -1;
        }

        int x0 = Hex2Int(s[0]);
        int x1 = Hex2Int(s[1]);
        int x2 = Hex2Int(s[2]);
        int x3 = Hex2Int(s[3]);
        
        if(x0 < 0 || x1 < 0 || x2 < 0 || x3 < 0)
        {
            result = 0;
            return false;
        }

        result = (byte)(x0 * Math.Pow(16,3) + x1 * Math.Pow(16,2) + x2 * 16 + x3 * 1);
        return true;

    }

    static void ReadAddress(string message, string errorMsg, out Address addr)
    {
        Address outAddr = null;
        ReadValue(message, errorMsg, str => {
            str = str.ToUpper();
            if(registers.ContainsKey(str))
            {
                outAddr = new Address(str);
                return true;
            } 

            if(TryToAddress(str, out ushort adr))
            {
                outAddr = new Address(adr);
                return true;
            }

            return false;
        });
        addr = outAddr;
    }

    static void ReadValue(string message, string errorMsg, Func<string, bool> validate)
    {    
        while(true)
        {
            Console.Write(message);
            if(validate(Console.ReadLine()))
                break;
            Console.WriteLine(errorMsg);
        }
    }

    static string Str(this byte v)
    {
        return string.Format("0x{0:X2}", v);
    }

    static string Str(this ushort v) 
    {
        return string.Format("0x{0:X4}", v);
    }

    static void PrintMemory()
    {
        foreach(var reg in registers)
            Console.WriteLine("Wartosc rejestru {0}: {1}", reg.Key, reg.Value.Str());

        Console.WriteLine("RAM:");

        foreach(Address address in UsedAdresses)
            address.Print();
        
    }
}