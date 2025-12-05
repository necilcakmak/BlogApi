pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = "blog_project"
    }

    stages {
        stage('Clone Repository') {
            steps {
                git 'https://github.com/necilcakmak/BlogApi.git'
            }
        }

        stage('Build & Deploy') {
            steps {
                script {
                    dir('.') {
                        // Mevcut container'ları yeniden build edip restart eder
                        sh 'docker-compose build'
                        sh 'docker-compose up -d'
                    }
                }
            }
        }
    }

    post {
        failure {
            echo "Deployment failed!"
        }
        success {
            echo "Deployment succeeded!"
        }
    }
}
