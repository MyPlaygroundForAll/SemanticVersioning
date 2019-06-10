pipeline {
  agent any
  stages {
    stage('Checkout Repository') {
      steps {
        git(url: 'https://github.com/MyPlaygroundForAll/SemanticVersioning.git', branch: 'master', poll: true)
      }
    }
    stage('Restore Nuget') {
      agent any
      steps {
        bat(script: 'C:\\NugetCLI\\nuget.exe restore SemanticVersioning.sln', returnStatus: true, returnStdout: true)
      }
    }
  }
}