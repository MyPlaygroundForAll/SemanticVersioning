pipeline {
  agent any
  stages {
    stage('Checkout Branch') {
      steps {
        git(url: 'https://github.com/MyPlaygroundForAll/SemanticVersioning.git', branch: 'master', poll: true)
      }
    }
    stage('Restore Nuget') {
		steps {
			bat 'C:\\NugetCLI\\nuget.exe restore SemanticVersioning.sln'
		}
    }
	stage('Update Assembly Version'){
		steps {
			bat('C:\\GitVersion\\GitVersion.exe /updateassemblyinfo')
		}
	}
	stage('Build Project'){
		steps {
			def msbuild = tool name: 'MsBuild', type: 'hudson.plugins.msbuild.MsBuildInstallation'
			bat "\"${msbuild}\" SemanticVersioning.sln /p:Configuration=Release"
		}
	}
	stage('Create Package'){
        steps {
			bat 'C:\\NugetCLI\\NuGet.exe pack src/SemanticVersioning.Core/SemanticVersioning.Core.csproj -IncludeReferencedProjects -Properties Configuration=Release'
		}
    }
	stage('Push Package'){
        environment {
            PANGEA_NUGET_KEY = credentials('Pangea_Nuget_Private_Key')
        }
        steps {
            def versionOutput = readJSON text: (bat(script: '@echo off && C:\\GitVersion\\GitVersion.exe /output json', returnStdout: true))
            print versionOutput.NugetVersion
            bat("C:\\NugetCLI\\NuGet.exe push SemanticVersioning.Core.${versionOutput.NuGetVersion}.nupkg ${PANGEA_NUGET_KEY} -Source http://pangeanuget.com/nuget")
        }
    }
  }
}