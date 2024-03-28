pipeline {
    agent any

    stages {
           stage("verify tooling") {
              steps {
                sh '''
                  docker version
                  docker info
                  docker compose version 
                  curl --version
                  jq --version
                '''
              }
            }
        
        stage('Clone Repository') {
            steps {
                git 'https://github.com/necilcakmak/BlogApi.git'
            }
        }

        stage('Run Docker Compose') {
            steps {
                script {
                    sh 'docker-compose up -d'
                }
            }
        }
    }

    post {
        always {
            script {
                sh 'docker-compose down'
            }
        }
    }
}
