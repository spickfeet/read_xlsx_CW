using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_read_xlsx.Tokens;

namespace CW_read_xlsx.State
{
    class OpenBracketState : IState
    {
        public void Enter(StateMachine sm)
        {
            if (sm.CurrentTokenIndex >= sm.TokensData.Count)
            {
                sm.ErrorsData.Add(new(sm.TokensData[sm.CurrentTokenIndex - 1].Line,
                    sm.TokensData[sm.CurrentTokenIndex - 1].LocalIndex + sm.TokensData[sm.CurrentTokenIndex - 1].TokenValue.Length,
                    $"Не хватает аргумента.",
                    sm.TokensData[sm.CurrentTokenIndex - 1].GlobalIndex + sm.TokensData[sm.CurrentTokenIndex - 1].TokenValue.Length));
                sm.IsStopped = true;
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.ARGUMENT)
            {
                sm.State = new ArgumentState();
            }
            else
            {
                sm.ErrorsData.Add(new(sm.TokensData[sm.CurrentTokenIndex].Line,
                    sm.TokensData[sm.CurrentTokenIndex].LocalIndex,
                    $"<{sm.TokensData[sm.CurrentTokenIndex].TokenValue}> не является ожидаемым, ожидается <аргумент>.",
                    sm.TokensData[sm.CurrentTokenIndex].GlobalIndex + sm.TokensData[sm.CurrentTokenIndex].TokenValue.Length - 1));
                sm.IsStopped = true;
                return;
            }
        }
    }
}
