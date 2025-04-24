using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CW_read_xlsx.Tokens;

namespace CW_read_xlsx.State
{
    public class StateMachine
    {
        public bool IsStopped {  get; set; }
        public IState State { get; set; }
        public List<TokenData> TokensData { get; set; }
        public int CurrentTokenIndex { get; set; }
        public List<ErrorData> ErrorsData {  get; set; }
        public StateMachine(List<TokenData> tokensData, List<ErrorData> errorsData)
        {
            ErrorsData = errorsData;
            TokensData = tokensData;
            State = new StartState();
        }

        public void Start()
        {
            CurrentTokenIndex = 0;
            IsStopped = false;
            CurrentTokenIndex = 0;
            for (; IsStopped == false; CurrentTokenIndex++)
            {
                State.Enter(this);
            }
        }
    }
}
