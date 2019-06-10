pipeline {
  agent any
  stages {
    stage('Checkout Repository') {
      steps {
        git(url: 'https://github.com/MyPlaygroundForAll/SemanticVersioning.git', branch: 'master', poll: true)
      }
    }
  }
}