using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW_read_xlsx.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace CW_read_xlsx.State
{
    public class StartState : IState
    {
        public void Enter(StateMachine sm)
        {
            if (sm.CurrentTokenIndex >= sm.TokensData.Count)
            {
                sm.IsStopped = true;
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.READ)
            {
                sm.State = new ReadState();
            }
            else 
            {
                sm.ErrorsData.Add(new(sm.TokensData[sm.CurrentTokenIndex].Line, 
                    sm.TokensData[sm.CurrentTokenIndex].LocalIndex,
                    $"<{sm.TokensData[sm.CurrentTokenIndex].TokenValue}> не является ожидаемым, ожидается <read_xlsx>.", 
                    sm.TokensData[sm.CurrentTokenIndex].GlobalIndex + sm.TokensData[sm.CurrentTokenIndex].TokenValue.Length - 1));
                sm.IsStopped = true;
                return;
            }
        }
    }
}
