﻿using Smartflow.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Dapper;
namespace Smartflow.Elements
{
    public class End :Node
    {
        [XmlIgnore]
        public override string NAME
        {
            get{ return "结束";}
        }


   
        [XmlIgnore]
        public override List<Transition> Transitions
        {
            get;
            set;
        }

        public override WorkflowNodeCategeory NodeType
        {
            get{return WorkflowNodeCategeory.End;}
        }
    }
}