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
                    // Host Docker kullanımı için docker.sock mount edilmiş olmalı
                    sh 'docker-compose build'
                    sh 'docker-compose up -d'
                }
            }
        }
    }

    post {
        failure {
            script {
                echo "Deployment failed!"
                // Opsiyonel: failure durumunda kapatma
                // sh 'docker-compose down'
            }
        }
        success {
            echo "Deployment succeeded!"
        }
    }
}
