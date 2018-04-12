﻿using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Dapper;
using Smartflow.Enums;

namespace Smartflow
{
    public class WorkflowNode : Node, IActor
    {
        protected WorkflowNode()
        {

        }

        public Transition Previous
        {
            get;
            set;
        }

        #region 节点方法

        public static WorkflowNode GetWorkflowNodeInstance(ASTNode node)
        {
            WorkflowNode wfNode = new WorkflowNode();
            wfNode.NID = node.NID;
            wfNode.ID = node.ID;
            wfNode.NAME = node.NAME;
            wfNode.NodeType = node.NodeType;
            wfNode.INSTANCEID = node.INSTANCEID;
            wfNode.Transitions = wfNode.QueryWorkflowNode(node.NID);
            wfNode.Previous = wfNode.GetHistoryTransition();
            return wfNode;
        }

        /// <summary>
        /// 获取回退线路
        /// </summary>
        /// <returns>路线</returns>
        protected Transition GetHistoryTransition()
        {
            Transition transition = null;
            WorkflowProcess process = WorkflowProcess.GetWorkflowProcessInstance(INSTANCEID, NID);
            if (process != null || NodeType != WorkflowNodeCategeory.Start)
            {
                ASTNode n = GetNode(process.FROM);
                while (n.NodeType == WorkflowNodeCategeory.Decision)
                {
                    process = WorkflowProcess.GetWorkflowProcessInstance(INSTANCEID, n.NID);
                    n = GetNode(process.FROM);

                    if (n.NodeType == WorkflowNodeCategeory.Start)
                        break;
                }
                transition = GetTransition(process.TID);
            }

            return transition;
        }

        /// <summary>
        /// 依据主键获取路线
        /// </summary>
        /// <param name="TID">路线主键</param>
        /// <returns>路线</returns>
        protected Transition GetTransition(long TID)
        {
            string query = "SELECT * FROM T_TRANSITION WHERE NID=@TID AND INSTANCEID=@INSTANCEID";
            Transition transition = Connection.Query<Transition>(query, new
            {
                TID = TID,
                INSTANCEID = INSTANCEID

            }).FirstOrDefault();

            return transition;
        }

        public List<WorkflowActor> GetActors()
        {
            string query = " SELECT * FROM T_ACTOR WHERE RNID=@RNID AND INSTANCEID=@INSTANCEID ";
            return Connection.Query<WorkflowActor>(query, new
            {
                RNID = NID,
                INSTANCEID = INSTANCEID

            }).ToList();
        }

        /// <summary>
        /// 检测当前节点是否有审批权限
        /// </summary>
        /// <param name="actorID">当前审批人ID</param>
        /// <returns>true：有 false：没有</returns>
        public bool CheckActor(long actorID)
        {
            if (NodeType == WorkflowNodeCategeory.Start || NodeType == WorkflowNodeCategeory.Decision) return true;
            string sql = " SELECT COUNT(*) FROM T_ACTOR WHERE ID=@ID AND RNID=@RNID AND INSTANCEID=@INSTANCEID ";

            return Connection.ExecuteScalar<int>(sql, new
            {
                RNID = NID,
                ID = actorID,
                INSTANCEID = INSTANCEID
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
            }) > 0;
        }

        /// <summary>
        /// 获取下一节点审批人员列表
        /// </summary>
        /// <param name="ID">下一节点ID</param>
        /// <returns>列表</returns>
        public List<WorkflowActor> GetNextActors(long ID)
        {
            ASTNode node = this.GetNode(ID);
            return new List<WorkflowActor>();
        }
        #endregion
    }
}