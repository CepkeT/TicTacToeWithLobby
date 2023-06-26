using FirstWebApp.ServerDatabase;
using FirstWebApp.Utilites;
using System;

namespace FirstWebApp.Models
{
    public class GameInfo
    {
        public Guid PlayerXGuid { get; private set; }
        public Guid PlayerOGuid { get; private set; }
        public Guid TurnPlayerGuid { get; private set; }

        public string PlayerXName => Database.Users[PlayerXGuid];
        public string PlayerOName => Database.Users[PlayerOGuid];

        public char[] DecodedField { get; private set; } = new char[9];
        public int EncodedField { get; private set; }

        public char TurnPlayerSymbol => TurnPlayerGuid == PlayerXGuid ? 'X' : 'O';
        public int TurnNumber => DecodedField.Count(x => x != ' ');

        public void SetField(int encodedField)
        {
            if (encodedField == EncodedField)
            {
                throw new ArgumentException(
                    $"{nameof(encodedField)} should change the state/value/field, but it is not");
            }

            EncodedField = encodedField;
            DecodedField = FieldDecodeUtility.DecodeField(encodedField);
        }

        public void SetField(char[] decodedField)
        {
            
            if (new String(decodedField) == new String(DecodedField))
            {
                throw new ArgumentException(
                    $"{nameof(decodedField)} should change the state/value/field, but it is not");
            }

            DecodedField = decodedField;
            EncodedField = FieldDecodeUtility.EncodeField(decodedField);
        }

        private void GenerateRandomField()
        {
            var field = new char[9];
            var randomizer = new Random();

            for (int counter = 0;counter < field.Length; counter++)
            {
                field[counter] = randomizer.Next(0, 3) switch
                {
                    0 => ' ',
                    1 => 'X',
                    _ => 'O'
                };
            }

            SetField(field);
        }

        public GameInfo()
        {
            PlayerXGuid = Guid.Empty;
            PlayerOGuid = Guid.Empty;
            TurnPlayerGuid = Guid.Empty;

            GenerateRandomField();
        }

        public GameInfo(Guid playerXGuid, Guid playerOGuid, Guid turnPlayerGuid)
        {
            PlayerXGuid = playerXGuid;
            PlayerOGuid = playerOGuid;
            TurnPlayerGuid = turnPlayerGuid;

            ///X O X
            ///O   O
            ///  X
            ///  
            ///{'X', 'O', 'X', 'O', ' ', 'O', ' ', 'X', ' '}
            ///
            /// x= 01
            /// o= 10
            ///  = 00
            ///  
            GenerateRandomField();
            //SetField(0b_01_10_01_10_00_10_00_01_00);
            //SetField(new[] { 'X', 'O', 'X', 'O', ' ', 'O', ' ', 'X', 'X' });
            //SetField(0b_01_10_01_10_00_10_00_01_01);

        }

    }
}
