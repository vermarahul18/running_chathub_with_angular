using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tryingWithAngularChat.Models;

namespace tryingWithAngularChat.Services
{
    public interface IChatService
    {
        // workspace related task
        Workspace CreateWorkspace(Workspace workspace);
        void DeleteWorkspace(string workspaceName);
        IEnumerable<Workspace> GetAllWorkspaces();
        Workspace GetWorkspaceById(string id);
        Workspace GetWorkspaceByName(string workspaceName);


        //// not implemented
        //Task<Workspace> UpdateWorkspaceByName(string workspaceName);
        ////
         
        //// channel related task
        Channel CreateChannel(Channel channel, string workspaceId);
        Channel GetChannelById(string channelId);
        //Task DeleteChannel(int channelId, string workspaceName);
        //Task<Channel> UpdateChannel(Channel channel);
        //Task<List<Channel>> GetChannelByuserIDandWorkspaceName(int userId, string workspaceName);
        User AddUserToChannel(User user, string channelId);
       // void DeleteUserFromChannel(string userName, int channelId);
        //Task<List<Message>> GetMessagesInChannel(int channelId, string workspaceName);

        //// user related task
        List<User> GetAllUsersInWorkspace(string workspaceName);
        User AddUserToWorkspace(string workspaceName, User user);  ////????? 
        //Task DeleteUserFromWorkspace(string workspaceName, int userId);
        //Task UpdateUserInWorkspace(User user);

        ////Message related task
        //Task DeleteMessageInChannel(string workspaceName, int channelId, int messageId);

    }
}
