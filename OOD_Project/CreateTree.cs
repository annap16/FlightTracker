using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class AbstractTree<T> where T : IGetFields, new()
    {
        public abstract void CreateTree(string query, Context<T> context);

        public string[] InfixNotationSplit(string query)
        {
            string[]pom = query.Split(' ');
            int ind = 0;
            for(; ind<pom.Length;ind++)
            {
                if (pom[ind]=="where")
                {
                    ind++;
                    break;
                }
            }
            List<string> result = new List<string>();
            string expression = "";
            for(;ind<pom.Length;ind++)
            {
                if (pom[ind].ToLower()=="and")
                {
                    result.Add(expression);
                    result.Add(pom[ind]);
                    expression = "";
                }
                else if (pom[ind].ToLower()=="or")
                {
                    result.Add(expression);
                    result.Add(pom[ind]);
                    expression = "";
                }
                else
                {
                    expression = expression + " " + pom[ind];
                }
            }
            result.Add(expression);
            return result.ToArray();
        }

        public string[] InfixToPostfix(string[] expression)
        {
            List<string> output = new List<string>();
            Stack<string> S = new Stack<string>();
            for(int i=0; i<expression.Length;i++)
            {
                if (expression[i].ToLower()=="and")
                {
                    while(S.Count()>0  && S.Peek().ToLower()=="and")
                    {
                        output.Add(S.Pop());
                    }
                    S.Push(expression[i]);
                }
                else if (expression[i].ToLower()=="or")
                {
                   
                    while(S.Count()>0)
                    {
                        output.Add(S.Pop());
                    }
                    S.Push(expression[i]);
                }
                else
                {
                    output.Add(expression[i]);
                }
            }
            while(S.Count>0)
            {
                output.Add(S.Pop());
            }
            return output.ToArray();
        }

        public WhereExpression<T> CreateTreeWhereExp(string[] data)
        {
            Stack<string> S = new Stack<string>();
            Stack<LogicExpression<T>> logicS = new Stack<LogicExpression<T>>();
            for(int i=0; i<data.Length;i++)
            {
                if (data[i].ToLower()!="and" && data[i].ToLower()!="or")
                {
                    S.Push(data[i]);
                }
                else
                {
                    if(logicS.Count()>0)
                    {
                        ConditionExpression<T> condExp1 = new ConditionExpression<T>(S.Pop());
                        if (data[i].ToLower()=="and")
                        {
                            AndExpression<T> andExp = new AndExpression<T>(logicS.Pop(), condExp1);
                            logicS.Push(andExp);
                        }
                        else
                        {
                            OrExpression<T> orExp = new OrExpression<T>(logicS.Pop(), condExp1);
                            logicS.Push(orExp);
                        }
                    }
                    else
                    {
                        ConditionExpression<T> condExp1 = new ConditionExpression<T>(S.Pop());
                        ConditionExpression<T> condExp2 = new ConditionExpression<T>(S.Pop());
                        if (data[i].ToLower() == "and")
                        {
                            AndExpression<T> andExp = new AndExpression<T>(condExp2, condExp1);
                            logicS.Push(andExp);
                        }
                        else
                        {
                            OrExpression<T> orExp = new OrExpression<T>(condExp2, condExp1);
                            logicS.Push(orExp);
                        }   
                    }
                }
            }
            if(logicS.Count()!=0)
                return new WhereExpression<T>(logicS.Pop());
            return new WhereExpression<T>(new ConditionExpression<T>(data[0]));
        }
    }

    public class DisplayTree<T>:AbstractTree<T> where T : IGetFields, new()
    {
        public override void CreateTree(string query, Context<T> context)
        {
            string cleanedInput = Regex.Replace(query, @"[\{\}\[\]\(\)\,]", "");
            string[] stringDiv = cleanedInput.Split(' ');
            int i = 1;
            for (; i < stringDiv.Length; i++)
            {
                if (stringDiv[i] == "from")
                    break;
                context.fieldsQuery.Add(stringDiv[i]);
            }
            string[] infixNotation = InfixNotationSplit(cleanedInput);
            if (infixNotation[0] =="")
            {
                FieldsExpression<T> fieldsNoWhere = new FieldsExpression<T>();
                DisplayExpression<T> dispNoWhere = new DisplayExpression<T>(fieldsNoWhere);
                for(int j=0; j<context.elementsFlag.Length;j++)
                {
                    context.elementsFlag[j] = true;
                }
                
                dispNoWhere.InterpretExp(context);
                return;
            }
            string[] postfixNotation = InfixToPostfix(infixNotation);
            WhereExpression<T> whereExp = CreateTreeWhereExp(postfixNotation);
            FieldsExpression<T> fieldsExp = new FieldsExpression<T>(whereExp);
            DisplayExpression<T> dispExp = new DisplayExpression<T>(fieldsExp);
            dispExp.InterpretExp(context);     
        }
    }

    public class UpdateTree<T>:AbstractTree<T> where T : IGetFields, new()
    {
        public override void CreateTree(string query, Context<T> context)
        {
            string cleanedInput = Regex.Replace(query, @"[\{\}\[\]\(\)]", "");
            string[] stringDiv = cleanedInput.Split(' ');
            int i = 0;
            string setQuery = "";
            for(;i<stringDiv.Length;i++)
            {
                if (stringDiv[i] == "set")
                {
                    i++;
                    break;
                }
            }
            for(;i<stringDiv.Length;i++)
            {
                if (stringDiv[i] == "where")
                    break;
                setQuery = setQuery + stringDiv[i] + " ";
            }
            setQuery = setQuery.Trim();
            string[] infixNotation = InfixNotationSplit(cleanedInput);
            if (infixNotation[0] == "")
            {
                SetExpression<T> setNoWhere = new SetExpression<T>(setQuery);
                UpdateExpression<T> updateNoWhere = new UpdateExpression<T>(setNoWhere);
                for (int j = 0; j < context.elementsFlag.Length; j++)
                {
                    context.elementsFlag[j] = true;
                }
                updateNoWhere.InterpretExp(context);
                return;
            }
            string[] postfixNotation = InfixToPostfix(infixNotation);
            WhereExpression<T> whereExp = CreateTreeWhereExp(postfixNotation);
            SetExpression<T> setExp = new SetExpression<T>(whereExp, setQuery);
            UpdateExpression<T> updateExp = new UpdateExpression<T>(setExp);
            updateExp.InterpretExp(context);
        }
    }

    public class DeleteTree<T> : AbstractTree<T> where T : IGetFields, new()
    {
        public override void CreateTree(string query, Context<T> context)
        {
            string cleanedInput = Regex.Replace(query, @"[\{\}\[\]\(\)\,]", "");
            string[] infixNotation = InfixNotationSplit(cleanedInput);
            if (infixNotation[0] == "")
            {
                DeleteExpression<T> delNoWhere = new DeleteExpression<T>();
                for (int j = 0; j < context.elementsFlag.Length; j++)
                {
                    context.elementsFlag[j] = true;
                }

                delNoWhere.InterpretExp(context);
                return;
            }
            string[] postfixNotation = InfixToPostfix(infixNotation);
            WhereExpression<T> whereExp = CreateTreeWhereExp(postfixNotation);
            DeleteExpression<T> delExp = new DeleteExpression<T>(whereExp);
            delExp.InterpretExp(context);
        }
    }

    public class AddTree<T> : AbstractTree<T> where T : IGetFields, new()
    {
        public override void CreateTree(string query, Context<T> context)
        {
            string cleanedInput = Regex.Replace(query, @"[\{\}\[\]\(\)]", "");
            string[] stringDiv = cleanedInput.Split(' ');
            int i = 0;
            string setQuery = "";
            for (; i < stringDiv.Length; i++)
            {
                if (stringDiv[i] == "new")
                {
                    i++;
                    break;
                }
            }
            for (; i < stringDiv.Length; i++)
            {
                setQuery = setQuery + stringDiv[i] + " ";
            }
            setQuery = setQuery.Trim();
            SetExpression<T> setExp = new SetExpression<T>(setQuery);
            AddExpression<T> addExp = new AddExpression<T>(setExp);
            addExp.InterpretExp(context);
            return;
        }
    }
}
