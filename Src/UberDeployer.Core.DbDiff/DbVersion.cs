﻿using System;
using System.Text.RegularExpressions;

namespace UberDeployer.Core.DbDiff
{
  public class DbVersion : IComparable<DbVersion>
  {
    private static readonly Regex _VersionRegex = new Regex(@"^(?<Major>[0-9]+)(\.(?<Minor>[0-9]+))?(\.(?<Revision>[0-9]+))?(\.(?<Build>[0-9]+))?(?<Tail>.*)$", RegexOptions.Compiled);

    #region Constructor(s)

    public DbVersion(int major, int minor = 0, int revision = 0, int build = 0, string tail = "")
    {
      Major = major;
      Minor = minor;
      Revision = revision;
      Build = build;
      Tail = tail;
    }

    #endregion

    #region Public methods

    public static DbVersion FromString(string versionStr)
    {
      if (string.IsNullOrEmpty(versionStr)) throw new ArgumentException("Argument can't be null nor empty.", "versionStr");

      Match match = _VersionRegex.Match(versionStr);

      if (!match.Success)
      {
        throw new FormatException("Version string is not in correct format. Expected: 'Major[.Minor][.Revision][.Build]'.");
      }

      string majorStr = match.Groups["Major"].Value;
      string minorStr = match.Groups["Minor"].Value;
      string revisionStr = match.Groups["Revision"].Value;
      string buildStr = match.Groups["Build"].Value;
      string tail = match.Groups["Tail"].Value;

      return
        new DbVersion(
          int.Parse(majorStr),
          !string.IsNullOrEmpty(minorStr) ? int.Parse(minorStr) : 0,
          !string.IsNullOrEmpty(revisionStr) ? int.Parse(revisionStr) : 0,
          !string.IsNullOrEmpty(buildStr) ? int.Parse(buildStr) : 0,
          tail);
    }

    public bool IsSmallerThan(DbVersion otherVersion)
    {
      if (otherVersion == null) throw new ArgumentNullException("otherVersion");

      return Major < otherVersion.Major
          || (Major == otherVersion.Major && Minor < otherVersion.Minor)
          || (Minor == otherVersion.Minor && Revision < otherVersion.Revision)
          || (Revision == otherVersion.Revision && Build < otherVersion.Build)
          || (Build == otherVersion.Build && String.CompareOrdinal(Tail, otherVersion.Tail) < 0);
    }

    public bool IsEqualTo(DbVersion otherVersion)
    {
      return Equals(otherVersion);
    }

    public bool IsGreatherThan(DbVersion dbVersion)
    {
      return !IsSmallerThan(dbVersion) && !IsEqualTo(dbVersion);
    }

    #endregion

    #region IComparable<DbVersion> members

    // TODO IMM HI: what about nulls?
    public int CompareTo(DbVersion other)
    {
      return IsSmallerThan(other) ? -1 : 1;
    }

    #endregion

    #region Object overrides

    public override string ToString()
    {
      return string.Format("{0}.{1}.{2}.{3}.{4}", Major, Minor, Revision, Build, Tail);
    }

    #endregion

    #region Equality members

    public bool Equals(DbVersion other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return other.Major == Major && other.Minor == Minor && other.Revision == Revision && other.Build == Build && Equals(other.Tail, Tail);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (DbVersion)) return false;
      return Equals((DbVersion) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int result = Major;
        result = (result*397) ^ Minor;
        result = (result*397) ^ Revision;
        result = (result*397) ^ Build;
        result = (result*397) ^ (Tail != null ? Tail.GetHashCode() : 0);
        return result;
      }
    }

    #endregion

    #region Properties

    public int Major { get; private set; }

    public int Minor { get; private set; }

    public int Revision { get; private set; }

    public int Build { get; private set; }

    public string Tail { get; private set; }

    #endregion
  }
}
