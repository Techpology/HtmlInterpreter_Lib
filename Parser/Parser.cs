using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace htmlInterpreter.Parser
{
    public class Parser
    {
        string parserName { get; set; }
        string version { get; set; }

        public class parserRules
        {
            public string[] syntax { get; set; }
            public string[] syntaxN { get; set; }
            public string[][] grammar { get; set; }
            public int[][] grammarV { get; set; }
            public string[] grammarN { get; set; }
        }

        /* Lexer/Parser grammar */
        public string grammarPath;
        public parserRules parserGrammar;

        public void getGrammar()
        {
            using(StreamReader sr = new StreamReader(grammarPath))
            {
                parserRules _o = JsonConvert.DeserializeObject<parserRules>(sr.ReadToEnd());
                parserGrammar = _o;
            }
        }

        public List<Token> lexifier(string _toLexify)
        {
            List<string> stack = new List<string>();
            List<Token> tokens = new List<Token>();

            for (int i = 0; i < _toLexify.Length; i++)
            {
                string _tli = _toLexify[i].ToString();
                string _tli1 = (_toLexify.Length - 1 != i) ? _toLexify[i+1].ToString() : "";
                if (parserGrammar.syntax.Contains(_tli) || parserGrammar.syntax.Contains(_tli+_tli1))
                {
                    if(stack.Count != 0)
                    {
                        Token _t = new Token("STR", string.Join("", stack));
                        tokens.Add(_t);
                        stack.Clear();
                    }
                    int _i = (parserGrammar.syntax.Contains(_tli+_tli1)) ? Array.IndexOf(parserGrammar.syntax, _tli + _tli1) : Array.IndexOf(parserGrammar.syntax, _tli);
                    i += (parserGrammar.syntax.Contains(_tli + _tli1)) ? 1 : 0;
                    Token _ts = new Token(parserGrammar.syntaxN[_i], parserGrammar.syntax[_i]);
                    tokens.Add(_ts);
                }
                else
                {
                    stack.Add(_tli);
                }
            }

            foreach (Token token in tokens)
            {
                Console.WriteLine(token.type + " _:_ " + token.value);
            }

            return tokens;
        }

        public void parse(List<Token> _tokens)
        {
            List<Exec> _execs = new List<Exec>();
            int index = 0;

            while(true)
            {
                int range = 0;
                for (int i = 0; i < parserGrammar.grammar.Length; i++)
                {
                    range = parserGrammar.grammar[i].Length;

                    if(!(index + range > _tokens.Count))
                    {
                        Console.WriteLine("here");
                        string _t = String.Join(",", Token.getTokenTypes(_tokens)[index..(index+range)]);
                        string _p = String.Join(",", parserGrammar.grammar[i]);

                        if(_t == _p)
                        {
                            Console.WriteLine($"found {_t}");
                            Exec _e = new Exec(parserGrammar.grammarN[i]);
                            for (int j = 0; j < parserGrammar.grammarV[i].Length; j++)
                            {
                                _e.vals.Add(_tokens[index + parserGrammar.grammarV[i][j]].value);
                            }
                            _execs.Add(_e);
                            index += range;
                            Console.WriteLine(index);
                        }
                    }
                }

                if (index >= _tokens.Count)
                {
                    Console.WriteLine("break 2");
                    Console.WriteLine(_tokens.Count);
                    Console.WriteLine(index);
                    Console.WriteLine(index + range);
                    break;
                }
            }
            Console.WriteLine("ended while");

            foreach (var item in _execs)
            {
                Console.WriteLine("exec: " + item.type + " - " + String.Join(" ",item.vals));
            }
        }

    }
}
