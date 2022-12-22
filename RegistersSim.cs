using System;
using System.Collections.Generic;

using Registry = System.Collections.Generic.Dictionary<string, byte>;

public static class RegistersSim
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

    public static void Run() 
    {
    
        Console.WriteLine("Wprowadz wartosci rejestrow");

        foreach(string key in new List<string>(registers.Keys))
        {
            byte register = 0;
            ReadValue(key + ": ", "Wpisz poprawny znak", str => TryToNumber(str, out register));
            registers[key] = register;
        }
    
        PrintReg();

        while(true)
        {
            Console.WriteLine("\n=============================");
            Console.WriteLine("Wybierz operacje: ");
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
            
            switch(Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                {
                    string srcReg = "", dstReg = "";
                    
                    ReadValue("Podaj rejestr docelowy: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if(!registers.ContainsKey(str))
                            return false;
                        
                        dstReg = str;
                        return true;
                    });

                    ReadValue("Podaj rejestr Ÿród³owy: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if(!registers.ContainsKey(str))
                            return false;
                        
                        srcReg = str;
                        return true;
                    });
                    
                    if(srcReg == dstReg)
                    {
                        Console.WriteLine("NOP");
                        break;
                    }

                    registers[dstReg] = registers[srcReg];
                    break;
                }
                
                case 2:
                {
                    string reg1 = "", reg2 = "";
                    
                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if(!registers.ContainsKey(str))
                            return false;
                        
                        reg1 = str;
                        return true;
                    });

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if(!registers.ContainsKey(str))
                            return false;
                        
                        reg2 = str;
                        return true;
                    });
                    
                    if(reg1 == reg2)
                    {
                        Console.WriteLine("NOP");
                        break;
                    }

                    byte tmp = registers[reg1];
                    registers[reg1] = registers[reg2];
                    registers[reg2] = tmp;
                    break;
                }

                case 3:
                {
                    string reg = "";

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg = str;
                        return true;
                    });

                    registers[reg] = (byte)~registers[reg];
                    break;
                }

                case 4:
                {
                    string reg = "";

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg = str;
                        return true;
                    });

                    registers[reg]++; 
                    break;
                }

                case 5:
                {
                    string reg = "";

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg = str;
                        return true;
                    });

                    registers[reg]--;
                    break;
                }

                case 6:
                {
                    string reg1 = "", reg2 = "";

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg1 = str;
                        return true;
                    });

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg2 = str;
                        return true;
                    });

                    if (reg1 == reg2)
                    {
                        Console.WriteLine("NOP");
                        break;
                    }

                    registers[reg1] = (byte)(registers[reg1] & registers[reg2]);
                    break;
                }

                case 7:
                {
                    string reg1 = "", reg2 = "";

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg1 = str;
                        return true;
                    });

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg2 = str;
                        return true;
                    });

                    if (reg1 == reg2)
                    {
                        Console.WriteLine("NOP");
                        break;
                    }

                    registers[reg1] = (byte)(registers[reg1] | registers[reg2]);
                    break;
                }
                
                case 8:
                {
                    string reg1 = "", reg2 = "";

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg1 = str;
                        return true;
                    });

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg2 = str;
                        return true;
                    });

                    if (reg1 == reg2)
                    {
                        Console.WriteLine("NOP");
                        break;
                    }

                    registers[reg1] = (byte)(registers[reg1] ^ registers[reg2]);
                    break;
                }
                
                case 9:
                {
                    string reg1 = "", reg2 = "";

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg1 = str;
                        return true;
                    });

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg2 = str;
                        return true;
                    });

                    if (reg1 == reg2)
                    {
                        Console.WriteLine("NOP");
                        break;
                    }

                    registers[reg1] = (byte)(registers[reg1] + registers[reg2]);
                    break;
                }
                
                case 10:
                {
                    string reg1 = "", reg2 = "";

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg1 = str;
                        return true;
                    });

                    ReadValue("Podaj rejestr: ", "Zly rejestr", str => {
                        str = str.ToUpper();
                        if (!registers.ContainsKey(str))
                            return false;

                        reg2 = str;
                        return true;
                    });

                    if (reg1 == reg2)
                    {
                        Console.WriteLine("NOP");
                        break;
                    }

                    registers[reg1] = (byte)(registers[reg1] - registers[reg2]);
                    break;
                }
                

                case 11:
                    PrintReg();
                    break;

                default:
                    return;
            }
        }
    }



    static void PrintReg()
    {
        foreach(var reg in registers)
            Console.WriteLine("Wartosc rejestru {0}: {1}", reg.Key, reg.Value.Str());
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
}