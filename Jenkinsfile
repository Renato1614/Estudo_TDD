pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore Dependencies') {
            steps {
                script {
                    sh 'dotnet restore'
                }
            }
        }

        stage('Build') {
            steps {
                script {
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Unit Tests') {
            steps {
                script {
                    sh 'dotnet test --configuration Release --no-build --collect:"XPlat Code Coverage"'
                }
            }
        }
    }
}
