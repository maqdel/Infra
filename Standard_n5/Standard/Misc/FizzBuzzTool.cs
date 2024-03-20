using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace maqdel.Infra.Misc
{
    /// <summary>
    /// Provides methods to generate the FizzBuzz results
    /// </summary>
    public class FizzBuzzTool : IFizzBuzzTool
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(FizzBuzzTool));

        private List<int> _numbers = new List<int>();

        private List<DivisorToken> _divisorTokens = new List<DivisorToken>();
                
        private List<string> GenerateFizzBuzz(List<int> numbers, List<DivisorToken> divisorTokenDtos)
        {
            _logger.Info("");
            List<string> answer = new List<string>();
            try
            {
                _numbers = numbers;
                _divisorTokens = divisorTokenDtos;

                bool hasDivisor = false;
                StringBuilder tokensValue = new StringBuilder();

                foreach (var number in _numbers)
                {
                    hasDivisor = false;
                    tokensValue = new StringBuilder();
                    foreach (var token in _divisorTokens)
                    {
                        if (number % token.Divisor == 0)
                        {
                            if (hasDivisor == false) hasDivisor = true;
                            tokensValue.Append(token.Token);
                        }
                    }
                    if (hasDivisor)
                    {
                        answer.Add(tokensValue.ToString());
                    }
                    else
                    {
                        answer.Add(number.ToString());
                    }
                }
            } catch { 
                throw; 
            }
            return answer;
        }

        private List<int> SetNumbersRange(int initial, int final) 
        {
            _logger.Info("SetNumbersRange");
            List<int> answer = new List<int>();
            try {
                for (int i = initial; i <= final; i++)
                {
                    answer.Add(i);
                }
            } catch { 
                throw; 
            }                                     
            return answer;
        }

        public List<DivisorToken> ClassicTokens()
        {
            _logger.Info("");
            return new List<DivisorToken>()
            {
                new DivisorToken
                {
                    Divisor = 3,
                    Token = "Fizz"
                },
                new DivisorToken
                {
                    Divisor = 5,
                    Token = "Buzz"
                },
            };
        }

        public List<string> Classic()
        {            
            _logger.Info("Classic");
            return GenerateFizzBuzz(SetNumbersRange(1, 100), ClassicTokens());
        }

        public List<string> CustomByRange(int Start, int End, List<DivisorToken> DivisorTokens)
        {
            _logger.Info("CustomByRange");
            return GenerateFizzBuzz(SetNumbersRange(Start, End), DivisorTokens);
        }
        
        public List<string> CustomSet(List<int> Numbers, List<DivisorToken> DivisorTokens)
        {
            _logger.Info("CustomSet");
            return GenerateFizzBuzz(Numbers, DivisorTokens);
        }
    }
}
