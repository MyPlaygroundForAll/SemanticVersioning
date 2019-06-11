def versioningInformation

pipeline {
  agent any
  parameters {
        string(name: 'PERSON', defaultValue: 'Mr Jenkins', description: 'Who should I say hello to?')

        text(name: 'BIOGRAPHY', defaultValue: '', description: 'Enter some information about the person')

        booleanParam(name: 'TOGGLE', defaultValue: true, description: 'Toggle this value')

        choice(name: 'CHOICE', choices: ['One', 'Two', 'Three'], description: 'Pick something')

        password(name: 'PASSWORD', defaultValue: 'SECRET', description: 'Enter a password')

        file(name: "FILE", description: "Choose a file to upload")
  }
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
	    environment{
	        msbuild = tool name: 'MsBuild', type: 'hudson.plugins.msbuild.MsBuildInstallation'
	    }
		steps {
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
            script {
               versionOutput = readJSON text: (bat(script: '@echo off && C:\\GitVersion\\GitVersion.exe /output json', returnStdout: true))
            }
            bat("C:\\NugetCLI\\NuGet.exe push SemanticVersioning.Core.${versionOutput.NuGetVersion}.nupkg ${PANGEA_NUGET_KEY} -Source http://pangeanuget.com/nuget")
        }
    }
  }
  post { 
    always { 
        echo 'Test Always!'
    }
  }
}