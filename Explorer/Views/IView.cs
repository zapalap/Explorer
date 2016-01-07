using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Views
{
    public interface IView<T>
    {
        void Draw(T model);
    }
}
