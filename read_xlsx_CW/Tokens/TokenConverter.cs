using CW_read_xlsx.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CW_read_xlsx.Tokens
{
    public static class TokenConverter
    {
        public static List<TokenData> CreateTokens(string text, List<ErrorData> lexicalErrors)
        {
            Regex regex = new(@"read_xlsx|\s|((?!(read_xlsx))[^();,\s])+|\(|\)|;|,");
            List<TokenData> tokens = new();
            int tempLineNumber = 1;
            int tempLineOffset = 0;
            int globalIndex = 0;

            Regex regexNonValid = new(@"[^\w\s]");

            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0)
                foreach (Match match in matches)
                {
                    if (match.Value == "read_xlsx")
                    {
                        tokens.Add(new(TokenEnum.READ, match.Value, tempLineNumber, tempLineOffset, globalIndex));
                        tempLineOffset += match.Value.Length;
                        globalIndex += match.Value.Length;
                    }
                    else if (match.Value == "(")
                    {
                        tokens.Add(new(TokenEnum.OPEN_BRACKET, match.Value, tempLineNumber, tempLineOffset, globalIndex));
                        tempLineOffset += match.Value.Length;
                        globalIndex += match.Value.Length;
                    }
                    else if (match.Value == ",")
                    {
                        tokens.Add(new(TokenEnum.COMMA, match.Value, tempLineNumber, tempLineOffset, globalIndex));
                        tempLineOffset += match.Value.Length;
                        globalIndex += match.Value.Length;
                    }
                    else if (match.Value == ")")
                    {
                        tokens.Add(new(TokenEnum.CLOSE_BRACKET, match.Value, tempLineNumber, tempLineOffset, globalIndex));
                        tempLineOffset += match.Value.Length;
                        globalIndex += match.Value.Length;
                    }
                    else if (match.Value == ";")
                    {
                        tokens.Add(new(TokenEnum.END, match.Value, tempLineNumber, tempLineOffset, globalIndex));
                        tempLineOffset += match.Value.Length;
                        globalIndex += match.Value.Length;
                    }
                    else if (match.Value == "\n")
                    {
                        tempLineNumber++;
                        tempLineOffset = 0;
                    }
                    else if (match.Value == "\r")
                    {
                        continue;
                    }
                    else if (string.IsNullOrWhiteSpace(match.Value))
                    {
                        tempLineOffset += match.Value.Length;
                        globalIndex += match.Value.Length;
                    }
                    else
                    {

                        string nonValidSymbols = "!@\"№#$%^:&?*\\/.<>{}[];:\'`~-+=~ёйцукенгшщзхъфывапролджэячсмитьбюЁЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ";
                        StringBuilder validLineSB = new StringBuilder();
                        string nonValidText = "";
                        int indexOffset = 0;

                        for (int i = 0; i < match.Value.Length; i++)
                        {
                            if (nonValidSymbols.Contains(match.Value[i]))
                            {
                                if(i == indexOffset)
                                {
                                    indexOffset++;
                                }
                                nonValidText += match.Value[i];
                                if(i + 1 != match.Value.Length && nonValidSymbols.Contains(match.Value[i + 1]))
                                {
                                    continue;
                                }
                                lexicalErrors.Add(new(tempLineNumber, tempLineOffset + i - nonValidText.Length + 1, 
                                    $"{nonValidText} не является валидным", globalIndex + i));
                                nonValidText = "";
                                continue;
                            }
                            validLineSB.Append(match.Value[i]);
                        }
                        tempLineOffset += indexOffset;
                        globalIndex += indexOffset;
                        string validLine = validLineSB.ToString();
                        foreach (Match subMatch in regex.Matches(validLine))
                        {
                            if (subMatch.Value == "read_xlsx")
                            {
                                tokens.Add(new(TokenEnum.READ, subMatch.Value, tempLineNumber, tempLineOffset, globalIndex));
                                tempLineOffset += subMatch.Value.Length;
                                globalIndex += subMatch.Value.Length;
                            }
                            else
                            {
                                int argStartIndex = 0;
                                for (int i = 0; i < match.Value.Length; i++)
                                {
                                    if (char.IsDigit(match.Value[i]))
                                    {
                                        argStartIndex++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (argStartIndex > 0)
                                {
                                    lexicalErrors.Add(new(tempLineNumber, tempLineOffset,
                                    $"Аргумент не может начинаться с числа", globalIndex + argStartIndex - 1));
                                }
                                tokens.Add(new(TokenEnum.ARGUMENT, subMatch.Value.Replace(" ", ""), tempLineNumber, tempLineOffset,globalIndex));
                                tempLineOffset += subMatch.Value.Length;
                                globalIndex += subMatch.Value.Length;
                            }
                        }
                    }
                }
            return tokens;

        }
    }
}
