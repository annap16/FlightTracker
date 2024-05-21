using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOD_Project
{
    public abstract class Expression<T> where T:IGetFields, new()
    {
        public abstract void InterpretExp(Context<T> context);

    }

    public class DisplayExpression<T>:Expression<T> where T : IGetFields, new()
    {
        FieldsExpression<T> fields;

        public DisplayExpression(FieldsExpression<T> _fields)
        {
            fields = _fields;
        }
        public override void InterpretExp(Context<T> context)
        {
            fields.InterpretExp(context);
            int indx = 0;
            int columnNumbers = 0;
            List<string> columnsNames = new List<string>();
            for(int i=0; i<context.fieldsFlag.Length;i++)
            {
                if (context.fieldsFlag[i])
                {
                    columnNumbers++;
                    columnsNames.Add(context.fieldsName[i]);
                }
            }
            int[] maxWidth = new int[columnNumbers];
            for(int i=0; i<maxWidth.Length;i++)
            {
                maxWidth[i] = columnsNames[i].Length;
            }
            int tableWidth = 0;
            int indxMaxWidth=0;
            for(int i=0;i<context.fieldsFlag.Length;i++)
            {
                if (context.fieldsFlag[i])
                {
                    for(int j=0; j<context.list.Count(); j++)
                    {
                        if (context.elementsFlag[j])
                        {
                            string[] values = context.list[j].GetValues();
                            if (values[i].Length > maxWidth[indxMaxWidth])
                            {
                                maxWidth[indxMaxWidth] = values[i].Length;
                            }
                        }
                    }
                    indxMaxWidth++;
                }
            }
            string[] format = new string[columnsNames.Count()];
            string[] formatHeadline = new string[columnsNames.Count()];
            for(int i=0;i<columnsNames.Count(); i++)
            {
                format[i] = "{"+0.ToString()+"," +maxWidth[i].ToString()+"}";
                formatHeadline[i] = "{" + 0.ToString() + "," + (-maxWidth[i]).ToString() + "}";
                tableWidth += maxWidth[i];
            }
            Console.Write("| ");

            for (int i=0;i<columnsNames.Count();i++)
            {
                Console.Write(formatHeadline[i], columnsNames[i]);
                Console.Write(" | ");
            }
            Console.WriteLine();
            Console.Write("|");
            List<int> columnEnd = new List<int>();
            columnEnd.Add(maxWidth[0] + 3);
            for(int i=1; i<maxWidth.Length;i++)
            {
                columnEnd.Add(columnEnd[i - 1] + maxWidth[i] + 3);
            }
            for(int i=1;i<tableWidth + 3*columnsNames.Count(); i++)
            {
                if (columnEnd.Contains(i))
                {
                    Console.Write("+");
                }
                else
                    Console.Write("-");
            }
            Console.WriteLine("|");
            foreach(var row in context.list)
            {
                if (context.elementsFlag[indx])
                {
                    string[] values = row.GetValues();
                    int indxCol = 0;
                    Console.Write("| ");
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (context.fieldsFlag[i])
                        {
                            if (values[i] != null)
                            {
                                Console.Write(format[indxCol], values[i]);
                                Console.Write(" | ");
                                indxCol++;
                            }
                        }   
                    }
                    Console.WriteLine();
                }
                indx++;
            }
        }
    }

    public class UpdateExpression<T>:Expression<T> where T : IGetFields, new()
    {
        SetExpression<T> set;

        public UpdateExpression(SetExpression<T> _set)
        {
            set = _set;
        }

        public override void InterpretExp(Context<T> context)
        {
            set.InterpretExp(context);
            for(int i=0; i<context.list.Count();i++)
            {
                if (context.elementsFlag[i])
                {
                    for(int j=0; j<context.setQuery.Count();j++)
                    {
                        if (context.findFieldOnName.setDictionaryUInt16.TryGetValue(context.setQuery[j].Item1, out var funcUIn16))
                        {
                            funcUIn16(context.list[i] as DataType, UInt16.Parse(context.setQuery[j].Item3));
                        }
                        else if (context.findFieldOnName.setDictionaryUInt64.TryGetValue(context.setQuery[j].Item1, out var funcUInt64))
                        {
                            funcUInt64(context.list[i] as DataType, UInt64.Parse(context.setQuery[j].Item3));
                        }
                        else if (context.findFieldOnName.setDictionarySingle.TryGetValue(context.setQuery[j].Item1, out var funcSingle))
                        {
                            funcSingle(context.list[i] as DataType, Single.Parse(context.setQuery[j].Item3));
                        }
                        else if (context.findFieldOnName.setDictionaryString.TryGetValue(context.setQuery[j].Item1, out var funcString))
                        {
                            funcString(context.list[i] as DataType, context.setQuery[j].Item3);
                        }
                        else
                        {
                            throw new Exception("Wrong set statement");
                        }
                    }
                }
            }
        }
    }

    public class DeleteExpression<T>:Expression<T> where T : IGetFields, new()
    {
        public WhereExpression<T> where;
        public DeleteExpression(WhereExpression<T> _where)
        {
            where = _where; 
        }
        public DeleteExpression()
        {
            where = null;
        }

        public override void InterpretExp(Context<T> context)
        {
            if (where != null)
                where.InterpretExp(context);
            context.currIndx = 0;
            for(int i=context.list.Count()-1; i>=0;i--)
            {
                if (context.elementsFlag[i])
                {
                    context.list.RemoveAt(i);
                }
            }
        }
    }

    public class AddExpression<T>:Expression<T> where T : IGetFields, new()
    {
        public SetExpression<T> newExp;

        public AddExpression(SetExpression<T> _newExp)
        {
            newExp = _newExp;
        }
        public override void InterpretExp(Context<T> context)
        {
            newExp.InterpretExp(context);
            if(context.fieldsName.Length!=context.setQuery.Count())
            {
                throw new Exception("You have to specify all fields if you want to add sth");
            }
            T newObj = new T();
            for(int j =0; j<context.setQuery.Count();j++)
            {
                if (context.findFieldOnName.setDictionaryUInt16.TryGetValue(context.setQuery[j].Item1, out var funcUIn16))
                {
                    funcUIn16(newObj as DataType, UInt16.Parse(context.setQuery[j].Item3));
                }
                else if (context.findFieldOnName.setDictionaryUInt64.TryGetValue(context.setQuery[j].Item1, out var funcUInt64))
                {
                    funcUInt64(newObj as DataType, UInt64.Parse(context.setQuery[j].Item3));
                }
                else if (context.findFieldOnName.setDictionarySingle.TryGetValue(context.setQuery[j].Item1, out var funcSingle))
                {
                    funcSingle(newObj as DataType, Single.Parse(context.setQuery[j].Item3));
                }
                else if (context.findFieldOnName.setDictionaryString.TryGetValue(context.setQuery[j].Item1, out var funcString))
                {
                    funcString(newObj as DataType, context.setQuery[j].Item3);
                }
                else
                {
                    throw new Exception("Wrong field name in add statement");
                }
            }
            context.list.Add(newObj);
        }
    }

    public class SetExpression<T>:Expression<T> where T : IGetFields, new()
    {
        WhereExpression<T> where;
        string setQuery;

        public SetExpression(WhereExpression<T> _where, string _setQuery)
        {
            where = _where;
            setQuery = _setQuery;
        }

        public SetExpression(string _setQuery)
        {
            setQuery = _setQuery;
            where = null;
        }

        public override void InterpretExp(Context<T> context)
        {
            if(where!=null)
                where.InterpretExp(context);
            string[] stringSplit = setQuery.Split(", ");
            for (int i=0; i<stringSplit.Length;i++)
            {
                
                string[] splitSetQuery = SplitExpression(stringSplit[i]);
                context.setQuery.Add((splitSetQuery[0], splitSetQuery[1], splitSetQuery[2]));
            }
        }

        public string[] SplitExpression(string exp)
        {               
            exp = exp.Trim();
            exp = exp.Replace(",", "");
            string pattern = @"^([^<>=!]+)(=)([^<>=!]+)$";
            Match match = Regex.Match(exp, pattern);
            if (match.Success)
            {
                return [match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value];
            }
            else
            {
                throw new Exception("Wrong condition in where");
            }
        }
    }

    public class FieldsExpression<T>:Expression<T> where T : IGetFields, new()
    {
        WhereExpression<T> where;

        public FieldsExpression(WhereExpression<T> _where)
        {
            where = _where;
        }

        public FieldsExpression()
        {
            where = null;
        }
        public override void InterpretExp(Context<T> context)
        {
            if(where!=null)
                where.InterpretExp(context);
            if(context.fieldsQuery.Count==1 && context.fieldsQuery[0]=="*")
            {
                for(int i=0; i<context.fieldsFlag.Length;i++)
                {
                    context.fieldsFlag[i] = true;
                }
                return;
            }
            for (int i=0; i < context.fieldsFlag.Length;i++)
            {
                if (context.fieldsQuery.Contains(context.fieldsName[i]))
                {
                    context.fieldsFlag[i] = true;
                }
            }     
        }

    }

    public class WhereExpression<T> : Expression<T> where T : IGetFields, new()
    {
        LogicExpression<T> logicExp;
        public WhereExpression(LogicExpression<T> _logicExp)
        {
            logicExp = _logicExp;
        }

        public override void InterpretExp(Context<T> context)
        {
            for(int i=0; i<context.elementsFlag.Length;i++)
            {
                context.currIndx = i;
                context.elementsFlag[i] = logicExp.InterpretExp(context);
            }
            
        }
    }

    public abstract class LogicExpression<T> where T : IGetFields
    {
        public LogicExpression()
        {
        }
        public abstract bool InterpretExp(Context<T> context);

    }

    public class OrExpression<T>:LogicExpression<T> where T : IGetFields
    {
        public LogicExpression<T> left;
        public LogicExpression<T> right;
        public OrExpression(LogicExpression<T> _left, LogicExpression<T> _right)
        {
            left = _left;
            right = _right;
        }

        public override bool InterpretExp(Context<T> context)
        {
            return left.InterpretExp(context)||right.InterpretExp(context);
        }

    }

    public class AndExpression<T>:LogicExpression<T> where T : IGetFields
    {
        public LogicExpression<T> left;
        public LogicExpression<T> right;
        public AndExpression(LogicExpression<T> _left, LogicExpression<T> _right)
        {
            left = _left;
            right = _right;
        }

        public override bool InterpretExp(Context<T> context)
        {
            return left.InterpretExp(context)&&right.InterpretExp(context);
        }

    }

    public class ConditionExpression<T> : LogicExpression<T> where T : IGetFields
    {
        string condition;

        public ConditionExpression(string _cond)
        {
            condition = _cond;
        }
        public override bool InterpretExp(Context<T> context)
        {
            string[] splitCondition = SplitExpression();
            int leftFieldInd = -1;
            int rightFieldInd = -1;
            for(int i=0; i<context.fieldsName.Length; i++)
            {
                if (splitCondition[0] == context.fieldsName[i]) 
                {
                    leftFieldInd = i;
                }
                if (splitCondition[2] == context.fieldsName[i])
                {
                    rightFieldInd = i;
                }
            }
            bool retFromFunc = false;
            if (leftFieldInd != -1)
            {
                if (context.findFieldOnName.dictionaryUInt64.TryGetValue(context.fieldsName[leftFieldInd], out var func))
                {
                    UInt64 parsedUInt64Left = func(context.list[context.currIndx] as DataType);
                    UInt64 parsedUInt64Right;
                    if (rightFieldInd != -1)
                    {
                        if (context.findFieldOnName.dictionaryUInt64.TryGetValue(context.fieldsName[rightFieldInd], out var func2))
                        {
                            parsedUInt64Right = func2(context.list[context.currIndx] as DataType);
                        }
                        else
                        {
                            throw new Exception("Cant compare different types in where condition");
                        }
                    }
                    else
                    {
                        parsedUInt64Right = UInt64.Parse(splitCondition[2]);
                    }
                    retFromFunc = GenericCompare<UInt64>.Compare(parsedUInt64Left, parsedUInt64Right, splitCondition[1]);
                }
                else if (context.findFieldOnName.dictionaryUInt16.TryGetValue(context.fieldsName[leftFieldInd], out var funcUInt16L))
                {
                    UInt16 parsedUInt16Left = funcUInt16L(context.list[context.currIndx] as DataType);
                    UInt16 parsedUInt16Right;
                    if (rightFieldInd != -1)
                    {
                        if (context.findFieldOnName.dictionaryUInt16.TryGetValue(context.fieldsName[rightFieldInd], out var funcUInt16R))
                        {
                            parsedUInt16Right = funcUInt16R(context.list[context.currIndx] as DataType);
                        }
                        else
                        {
                            throw new Exception("Cant compare different types in where condition");
                        }
                    }
                    else
                    {
                        parsedUInt16Right = UInt16.Parse(splitCondition[2]);
                    }
                    retFromFunc = GenericCompare<UInt16>.Compare(parsedUInt16Left, parsedUInt16Right, splitCondition[1]);
                }
                else if (context.findFieldOnName.dictionarySingle.TryGetValue(context.fieldsName[leftFieldInd], out var funcSingleL))
                {
                    Single parsedSingleLeft = funcSingleL(context.list[context.currIndx] as DataType);
                    Single parsedSingleRight;
                    if (rightFieldInd != -1)
                    {
                        if (context.findFieldOnName.dictionarySingle.TryGetValue(context.fieldsName[rightFieldInd], out var funcSingleR))
                        {
                            parsedSingleRight = funcSingleR(context.list[context.currIndx] as DataType);
                        }
                        else
                        {
                            throw new Exception("Cant compare different types in where condition");
                        }
                    }
                    else
                    {
                        parsedSingleRight = Single.Parse(splitCondition[2]);
                    }
                    retFromFunc = GenericCompare<Single>.Compare(parsedSingleLeft, parsedSingleRight, splitCondition[1]);
                }
                else if (context.findFieldOnName.dictionaryString.TryGetValue(context.fieldsName[leftFieldInd], out var funcStringL))
                {
                    string parsedStringLeft = funcStringL(context.list[context.currIndx] as DataType);
                    string parsedStringRight;
                    if (rightFieldInd != -1)
                    {
                        if (context.findFieldOnName.dictionaryString.TryGetValue(context.fieldsName[rightFieldInd], out var funcStringR))
                        {
                            parsedStringRight = funcStringR(context.list[context.currIndx] as DataType);
                        }
                        else
                        {
                            throw new Exception("Cant compare different types in where condition");
                        }
                    }
                    else
                    {
                        parsedStringRight = splitCondition[2];
                    }
                    retFromFunc = GenericCompare<string>.Compare(parsedStringLeft, parsedStringRight, splitCondition[1]);
                }
                else
                {
                    throw new Exception("Wrong name of a field in where");
                }
            }
            else if (rightFieldInd != -1)
            {
                if (context.findFieldOnName.dictionaryUInt64.TryGetValue(context.fieldsName[rightFieldInd], out var func))
                {
                    UInt64 parsedUInt64Right = func(context.list[context.currIndx] as DataType);
                    UInt64 parsedUInt64Left = UInt64.Parse(splitCondition[0]);
                    retFromFunc = GenericCompare<UInt64>.Compare(parsedUInt64Left, parsedUInt64Right, splitCondition[1]);
                }
                else if (context.findFieldOnName.dictionaryUInt16.TryGetValue(context.fieldsName[rightFieldInd], out var funcUInt16R))
                {
                    UInt16 parsedUInt16Right = funcUInt16R(context.list[context.currIndx] as DataType);
                    UInt16 parsedUInt16Left = UInt16.Parse(splitCondition[0]);
                    retFromFunc = GenericCompare<UInt16>.Compare(parsedUInt16Left, parsedUInt16Right, splitCondition[1]);
                }
                else if (context.findFieldOnName.dictionarySingle.TryGetValue(context.fieldsName[rightFieldInd], out var funcSingleR))
                {
                    Single parsedSingleRight = funcSingleR(context.list[context.currIndx] as DataType);
                    Single parsedSingleLeft = parsedSingleRight = Single.Parse(splitCondition[0]);
                    retFromFunc = GenericCompare<Single>.Compare(parsedSingleLeft, parsedSingleRight, splitCondition[1]);
                }
                else if (context.findFieldOnName.dictionaryString.TryGetValue(context.fieldsName[rightFieldInd], out var funcStringR))
                {
                    string parsedStringRight = funcStringR(context.list[context.currIndx] as DataType);
                    string parsedStringLeft = parsedStringRight = splitCondition[0];
                    retFromFunc = GenericCompare<string>.Compare(parsedStringLeft, parsedStringRight, splitCondition[1]);
                }
            }
            else
            {
                throw new Exception("Wrong where condition");
            }
            return retFromFunc;

        }

        public string[] SplitExpression()
        {
            condition = condition.Trim();
            string pattern = @"^([^<>=!]+)(<=|>=|<|>|=|!=)([^<>=!]+)$";
            Match match = Regex.Match(condition, pattern);

            if (match.Success)
            {
                return [match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value];
            }
            else
            {
                throw new Exception("Wrong condition in where");
            }
        }
    }





}
