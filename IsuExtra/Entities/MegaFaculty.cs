using System;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(Group group)
        {
            switch (group.Name[0])
            {
                case 'M':
                case 'K':
                    Name = "TINT";
                    return;
                case 'P':
                case 'D':
                case 'L':
                    Name = "KTY";
                    return;
                case 'N':
                case 'W':
                case 'V':
                    Name = "FTF";
                    return;
                case 'O':
                case 'T':
                case 'Z':
                    Name = "BTINS";
                    return;
                case 'R':
                case 'U':
                    Name = "FTMI";
                    return;
                case 'B':
                case 'H':
                    Name = "NOJ";
                    return;
                default:
                    throw new OGNPException(
                        "error: group's name is not correct");
            }
        }

        public MegaFaculty(string name)
        {
            if (name is "TINT" or "NOJ" or "FTMI" or "BTINS" or "FTF" or "KTY") Name = name;
            else throw new OGNPException("there is no such faculty");
        }

        public string Name { get; }
    }
}