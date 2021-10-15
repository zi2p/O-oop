using System;
using Isu.Entities;

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
                    Name = "FT";
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
                    throw new Exception(
                        "error: group's name is not correct");
            }
        }

        public MegaFaculty(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}