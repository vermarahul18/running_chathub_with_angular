using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tryingWithAngularChat.Models;
using MongoDB.Driver.Builders;
using MongoDB.Driver;


namespace tryingWithAngularChat.Services
{
    public class ChatService : IChatService
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;
        MongoDatabase _dbChannel;
        MongoDatabase _dbUser;
        MongoDatabase _dbMessage;

        public ChatService()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("AllWorkspace");
            _dbChannel = _server.GetDatabase("AllChannels");
            _dbUser = _server.GetDatabase("AllUsers");
            _dbMessage = _server.GetDatabase("AllMessages");

        }

        public IEnumerable<Workspace> GetAllWorkspaces()
        {
            return _db.GetCollection<Workspace>("Workspace").FindAll();
        }

        //public IEnumerable<ToDo> GetNotesByQuery(bool? Ispinned = null, string title = "", string labelName = "")
        //{
        //    return _db.GetCollection<ToDo>("Notes").FindAll().Where(
        //        m => ((title == "") || (m.Title == title)) && ((!Ispinned.HasValue) || (m.IsPinned == Ispinned)) && ((labelName == "") || (m.Labels).Any(b => b.LabelName == labelName)));
        //}

        public Workspace GetWorkspaceById(string id)
        {
            var result = Query<Workspace>.EQ(p => p.workspaceId, id);
            return _db.GetCollection<Workspace>("Workspace").FindOne(result);
        }

        public Workspace GetWorkspaceByName(string workspaceName)
        {
            var result = Query<Workspace>.EQ(p => p.workspaceName, workspaceName);
            return _db.GetCollection<Workspace>("Workspace").FindOne(result);
        }
        public Workspace CreateWorkspace(string workSpaceName)
        {
            Channel generalChannel = new Channel();
            generalChannel.channelName = "general";
            _dbChannel.GetCollection<Channel>("Channel").Save(generalChannel);
            Workspace newWorkSpace = new Workspace();
            newWorkSpace.workspaceName = workSpaceName;
            newWorkSpace.channels.Add(generalChannel);
            _db.GetCollection<Workspace>("Workspace").Save(newWorkSpace);
            return newWorkSpace;
        }
        public void DeleteWorkspace(string id)
        {
            var result = Query<Workspace>.EQ(e => e.workspaceId, id);
            var operation = _db.GetCollection<Workspace>("Workspace").Remove(result);
        }

        public Channel CreateChannel(Channel channel, string workspaceId)
        {
            _dbChannel.GetCollection<Channel>("Channel").Save(channel);
            var result = GetWorkspaceById(workspaceId);
            result.channels.Add(channel);
            result.workspaceId = workspaceId;
            var res = Query<Workspace>.EQ(pd => pd.workspaceId, workspaceId);
            var operation = Update<Workspace>.Replace(result);
            _db.GetCollection<Workspace>("Workspace").Update(res, operation);
            return channel;
        }
        public Channel GetChannelById(string channelId)
        {
            var result = Query<Channel>.EQ(p => p.channelId, channelId);
            return _dbChannel.GetCollection<Channel>("Channel").FindOne(result);
        }
        public User AddUserToChannel(User newUser, string channelId)
        {
           
            // add user to channel and updating channel
            var resultChannel = GetChannelById(channelId);
            resultChannel.users.Add(newUser);
            resultChannel.channelId = channelId;
            var res = Query<Channel>.EQ(pd => pd.channelId, channelId);
            var operation = Update<Channel>.Replace(resultChannel);
            _dbChannel.GetCollection<Channel>("Channel").Update(res, operation);

            // update channel in workspace
            var resultWorkspace = GetWorkspaceByName(newUser.workspaceName);
            resultWorkspace.channels.First(i => i.channelId == channelId).users.Add(newUser);
            var resWorkspace = Query<Workspace>.EQ(pd => pd.workspaceId, resultWorkspace.workspaceId);
            var operationWorkspace = Update<Workspace>.Replace(resultWorkspace);
            _db.GetCollection<Workspace>("Workspace").Update(resWorkspace, operationWorkspace);
            return newUser;

        }

        //public void DeleteUserFromChannel(string userName, int channelId)
        //{
        //    var result = Query<Channel>.EQ(e => e.channelId, channelId);
        //    var operation = _db.GetCollection<Workspace>("Workspace").Remove(result);
        //}

        public List<User> GetAllUsersInWorkspace(string workspaceName)
        {
            var result = Query<User>.EQ(p => p.workspaceName, workspaceName);
            var x =  _dbUser.GetCollection<User>("User").Find(result).ToList();
            return x;
        }

        public User AddUserToWorkspace(string workspaceName, User user)
        {
            _dbUser.GetCollection<User>("User").Save(user);
            var resultWorkspace = GetWorkspaceByName(workspaceName);
            resultWorkspace.channels.Find(i => i.channelName == "general").users.Add(user);
            var resworkspace = Query<Workspace>.EQ(pd => pd.workspaceName, workspaceName);
            var operationWorkspace = Update<Workspace>.Replace(resultWorkspace);
            _db.GetCollection<Workspace>("Workspace").Update(resworkspace, operationWorkspace);
            return user;
        }
    }
}