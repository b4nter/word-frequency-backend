using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCounter.objects;

namespace WordCounter.Interfaces
{
    public interface ITitleWordCounter
    {
        List<CountedWord> GetWords();
        void CountWords();
    }
}
