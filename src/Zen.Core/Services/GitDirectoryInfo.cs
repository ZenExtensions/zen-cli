using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zen.Core.Services
{

    public class GitDirectoryInfo
    {
        public string FullName { get => directory.FullName; }
        public string Name { get => directory.Name; }
        public bool IsGitDirectory { get; }
        public List<GitDirectoryInfo> SubDirectories { get; set; } = new List<GitDirectoryInfo>();
        public bool HasAnySubGitDirectories
        {
            get
            {
                if (SubDirectories is null)
                    return false;
                if (!SubDirectories.Any())
                    return false;
                var isAnySubDirectoryAGitDirectory = SubDirectories.Any(a => a.IsGitDirectory);
                if (isAnySubDirectoryAGitDirectory)
                    return true;
                var hasAnySubGitDirectories = SubDirectories.Any(a => a.HasAnySubGitDirectories);
                return hasAnySubGitDirectories;
            }
        }
        internal readonly DirectoryInfo directory;

        public GitDirectoryInfo(DirectoryInfo directory)
        {
            this.directory = directory;
            IsGitDirectory = GitDirectoryInfo.IsDirectoryAGitDirectory(directory);
            if(!IsGitDirectory)
                GitDirectoryInfo.populateSubDirectoryInfo(this);
        }

        static bool IsDirectoryAGitDirectory(DirectoryInfo directory)
        {
            return directory.EnumerateDirectories().Any(a => a.Name.Equals(".git"));
        }

        private static void populateSubDirectoryInfo(GitDirectoryInfo gitDirectory)
        {
            if(gitDirectory.IsGitDirectory)
                return;
            foreach (var item in gitDirectory.directory.EnumerateDirectories())
            {
                if(item.Name.StartsWith('.'))
                    continue;
                var subGitDirectory = new GitDirectoryInfo(item);
                if(subGitDirectory.IsGitDirectory || subGitDirectory.HasAnySubGitDirectories)
                {
                    gitDirectory.SubDirectories.Add(subGitDirectory);
                }
            }
        }
    }
}