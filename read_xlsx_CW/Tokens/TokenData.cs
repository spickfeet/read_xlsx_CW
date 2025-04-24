using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_read_xlsx.Tokens
{
    public class TokenData
    {
        [DisplayName("Значение")]
        public string TokenValue { get; set; }
        [DisplayName("Лексема")]
        public TokenEnum Token { get; set; }
        [DisplayName("Строка")]
        public int Line { get; set; }
        [DisplayName("Индекс в строке")]
        public int LocalIndex { get; set; }
        [DisplayName("Глобальный индекс")]
        public int GlobalIndex { get; set; }
        public TokenData(TokenEnum token, string tokenValue, int line, int localIndex, int globalIndex)
        {
            TokenValue = tokenValue;
            Token = token;
            Line = line;
            LocalIndex = localIndex;
            GlobalIndex = globalIndex;
        }
    }
}
