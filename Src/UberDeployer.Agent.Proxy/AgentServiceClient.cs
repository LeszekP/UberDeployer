﻿using System;
using System.Collections.Generic;
using UberDeployer.Agent.Proxy.Dto;
using UberDeployer.Agent.Proxy.Dto.TeamCity;

namespace UberDeployer.Agent.Proxy
{
  public class AgentServiceClient : WcfProxy<IAgentService>, IAgentService
  {
    #region IAgentService Members

    public void Deploy(Guid uniqueClientId, string requesterIdentity, string projectName, string projectConfigurationName, string projectConfigurationBuildId, string targetEnvironmentName)
    {
      Exec(@as => @as.Deploy(uniqueClientId, requesterIdentity, projectName, projectConfigurationName, projectConfigurationBuildId, targetEnvironmentName));
    }

    public void DeployAsync(Guid uniqueClientId, string requesterIdentity, string projectName, string projectConfigurationName, string projectConfigurationBuildId, string targetEnvironmentName)
    {
      Exec(@as => @as.DeployAsync(uniqueClientId, requesterIdentity, projectName, projectConfigurationName, projectConfigurationBuildId, targetEnvironmentName));
    }

    public List<ProjectInfo> GetProjectInfos(ProjectFilter projectFilter)
    {
      return Exec(@as => @as.GetProjectInfos(projectFilter));
    }

    public List<EnvironmentInfo> GetEnvironmentInfos()
    {
      return Exec(@as => @as.GetEnvironmentInfos());
    }

    public List<ProjectConfiguration> GetProjectConfigurations(string projectName, ProjectConfigurationFilter projectConfigurationFilter)
    {
      return Exec(@as => @as.GetProjectConfigurations(projectName, projectConfigurationFilter));
    }

    public List<ProjectConfigurationBuild> GetProjectConfigurationBuilds(string projectName, string projectConfigurationName, int maxCount, ProjectConfigurationBuildFilter projectConfigurationBuildFilter)
    {
      return Exec(@as => @as.GetProjectConfigurationBuilds(projectName, projectConfigurationName, maxCount, projectConfigurationBuildFilter));
    }

    public List<string> GetWebAppProjectTargetUrls(string projectName, string environmentName)
    {
      return Exec(@as => @as.GetWebAppProjectTargetUrls(projectName, environmentName));
    }

    public List<string> GetProjectTargetFolders(string projectName, string environmentName)
    {
      return Exec(@as => @as.GetProjectTargetFolders(projectName, environmentName));
    }

    public List<DeploymentRequest> GetDeploymentRequests(int startIndex, int maxCount)
    {
      return Exec(@as => @as.GetDeploymentRequests(startIndex, maxCount));
    }

    public List<DiagnosticMessage> GetDiagnosticMessages(Guid uniqueClientId, long lastSeenMaxMessageId, DiagnosticMessageType minMessageType)
    {
      return Exec(@as => @as.GetDiagnosticMessages(uniqueClientId, lastSeenMaxMessageId, minMessageType));
    }

    #endregion
  }
}
