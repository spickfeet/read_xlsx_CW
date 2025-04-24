using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_read_xlsx.State
{
    public interface IState
    {
        void Enter(StateMachine sm);
    }
}
