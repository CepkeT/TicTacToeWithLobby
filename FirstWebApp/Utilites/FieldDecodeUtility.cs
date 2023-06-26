namespace FirstWebApp.Utilites
{
    public static class FieldDecodeUtility
    {
        private static Dictionary<int, char> _cellDecodeDictionary = new Dictionary<int, char>
        {
            { 0, ' ' },
            { 1, 'X' },
            { 2, 'O' },
        };
        private static Dictionary<char, int> _cellEncodeDictionary = 
            _cellDecodeDictionary.ToDictionary(x => x.Value, x => x.Key);

        public static char[] DecodeField(int encodedField)
        {
            var result = new char[9];
            var toDecodeValue = encodedField;

            for (int counter = 8; counter >= 0; counter--)
            {
                var encodedCell = toDecodeValue - (toDecodeValue >> 2 << 2);
                toDecodeValue >>= 2;

                result[counter] = _cellDecodeDictionary[encodedCell];
            }

            return result;
        }

        public static int EncodeField(char[] decodedField)
        {
            int result = 0;

            for (int counter = 0; counter < decodedField.Length; counter++)
            {
                result <<= 2;

                result += _cellEncodeDictionary[decodedField[counter]];
            }

            return result;
        }

    }
}
