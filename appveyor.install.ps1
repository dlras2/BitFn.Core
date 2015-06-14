# Regular expression for matching semantic versioning (http://semver.org/)
$regex = "^v(?<semver>(?<version>(?<major>\d+)\.(?<minor>\d+)\.(?<revision>\d+))(?<prerelease>(?:\-[0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*)?)(?<metadata>(?:\+[0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*)?))($|(?=\s))"

# Create link to build log
$build_url = [string]::Format("{0}/project/{1}/{2}/build/{3}",
  $env:APPVEYOR_URL, $env:APPVEYOR_ACCOUNT_NAME, $env:APPVEYOR_PROJECT_SLUG, $env:APPVEYOR_BUILD_VERSION)

# Commit url
$commit_url = ""
If ($env:APPVEYOR_REPO_PROVIDER -eq "gitHub")
{
  $commit_url = [string]::Format("https://github/{0}/commit/{1}", $env:APPVEYOR_REPO_NAME, $env:APPVEYOR_REPO_COMMIT)
}

If (!$env:APPVEYOR_PULL_REQUEST_NUMBER -and ($env:APPVEYOR_REPO_BRANCH -eq "master") -and ($env:APPVEYOR_REPO_COMMIT_MESSAGE -match $regex))
{
  # Commit message consists of a 'v' followed by a valid semantic version string (e.g. "v1.23.4-beta+exp.sha.5114f85")

  # Treat this as a versioned deploy (possibly a prerelease version)
  $env:RELEASE = $true
  Write-Host "Preparing to release..."

  # Only update assembly version on breaking changes (major version increments)
  $env:ASSEMBLY_VERSION = $matches['major'] + ".0.0"
  # File version includes release number plus build number
  $env:ASSEMBLY_FILE_VERSION = $matches['version'] + "." + $env:APPVEYOR_BUILD_NUMBER
  # Assembly info version is equal to full semantic version plus build number metadata
  $env:ASSEMBLY_INFORMATIONAL_VERSION = $matches['semver']
  $delimiter = If (!!($matches['metadata'])) {"."} Else {"+"}
  $env:ASSEMBLY_INFORMATIONAL_VERSION = $env:ASSEMBLY_INFORMATIONAL_VERSION + $delimiter + $env:APPVEYOR_BUILD_NUMBER
  # NuGet doesn't allow dots in prerelease tags (-) or any build metadata (+)
  $env:PACKAGE_VERSION = $matches['version'] + ($matches['prerelease'] -replace "\.", "-")

  # Set up GitHub release properties
  $env:PRERELEASE = (!!($matches['prerelease']) -or ($matches['major'] -eq '0'))
  $env:RELEASE_TAG = "v" + $matches['semver']
  $env:RELEASE_TITLE = "Version " + $matches['semver']

  # Set release notes
  $env:RELEASE_NOTES = $env:APPVEYOR_REPO_COMMIT_MESSAGE_EXTENDED
}
Else
{
  # Do not assign a release number or deploy to NuGet
  $env:DEPLOY_NUGET = $false

  # Set assembly versions to "version-less" zero-versions
  $env:ASSEMBLY_VERSION = "0.0.0"
  $env:ASSEMBLY_FILE_VERSION = "0.0.0." + $env:APPVEYOR_BUILD_NUMBER
  $env:ASSEMBLY_INFORMATIONAL_VERSION = "0.0.0+" + ($env:APPVEYOR_REPO_BRANCH -replace "[^0-9A-Za-z-]", "") + "." + $env:APPVEYOR_BUILD_NUMBER
  # NuGet doesn't allow dots in prerelease tags (-) or any build metadata (+)
  $env:PACKAGE_VERSION = "0.0.0-" + ($env:APPVEYOR_REPO_BRANCH -replace "[^0-9A-Za-z]", "") + "-" + $env:APPVEYOR_BUILD_NUMBER

  # Set release notes
  $env:RELEASE_NOTES = [string]::Format("{0}\n\n[@{1}]({2})",
    $env:APPVEYOR_REPO_COMMIT_MESSAGE, ($env:APPVEYOR_REPO_COMMIT).substring(0,7), $commit_url)
}

# Append build log link to release notes
$env:RELEASE_NOTES = [string]::Format("{0}\n\n[Build #{1}]({2})", $env:RELEASE_NOTES, $env:APPVEYOR_BUILD_NUMBER, $build_url)

# Patch all .nuspec files
Get-ChildItem *.nuspec -Recurse | % {
  $xml = [xml](Get-Content $_)
  $xml.SelectNodes("//version") | % {$_.InnerText = ($env:PACKAGE_VERSION) }
  $xml.SelectNodes("//releaseNotes") | % {$_.InnerText = ($env:RELEASE_NOTES).Replace("\n", "`r`n") }
  $xml.Save($_)
}

Write-Host "Assembly version" $env:ASSEMBLY_VERSION
Write-Host "Assembly file version" $env:ASSEMBLY_FILE_VERSION
Write-Host "Assembly informational version" $env:ASSEMBLY_INFORMATIONAL_VERSION
Write-Host "Package version" $env:PACKAGE_VERSION
