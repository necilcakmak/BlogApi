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

        stage('Install Docker Compose') {
            steps {
                script {
                    // Docker Compose yüklü değilse kur
                    sh '''
                        if ! command -v docker-compose &> /dev/null
                        then
                            echo "Docker Compose bulunamadı, kuruluyor..."
                            apt-get update -y && apt-get install -y curl unzip
                            curl -L "https://github.com/docker/compose/releases/download/v2.35.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
                            chmod +x /usr/local/bin/docker-compose
                            docker-compose --version
                        else
                            echo "Docker Compose zaten yüklü"
                        fi
                    '''
                }
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
