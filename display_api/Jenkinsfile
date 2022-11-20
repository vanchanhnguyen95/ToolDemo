pipeline {
    environment {
    DOCKER_IMAGE = "registry.gitlab.com/luannguyenhuu97/sysadmin.isalerep"
    }
    agent any    
    stages {
        stage('Clone Source')
        {
            steps{
                git branch: 'main', credentialsId: 'gitlab_namht', url: 'https://gitlab.com/luannguyenhuu97/sysadmin.isalerep.git'
            }
        }
        stage('Build Images')
        {
            environment { 
                NAME = "registry.gitlab.com/luannguyenhuu97/sysadmin.isalerep/sysauth:1.0"
                DOCKER_TAG="${GIT_BRANCH.tokenize('/').pop()}-${GIT_COMMIT.substring(0,7)}"
            }
            steps
            {
               // This step should not normally be used in your script. Consult the inline help for details.
                withDockerRegistry(credentialsId: 'gitlab_token', url: 'https://registry.gitlab.com') {
                    sh "docker build -f Sys.Auth/Dockerfile -t  ${DOCKER_IMAGE}:${DOCKER_TAG} ."
                    sh "docker tag ${DOCKER_IMAGE}:${DOCKER_TAG} ${DOCKER_IMAGE}:latest" 
                    sh "docker image ls | grep ${DOCKER_IMAGE}"                
                    sh "docker push ${DOCKER_IMAGE}:${DOCKER_TAG} "
                    sh "docker push ${DOCKER_IMAGE}:latest"
                }
                //clean to save disk
                sh "docker image rm ${DOCKER_IMAGE}:${DOCKER_TAG}"
                sh "docker image rm ${DOCKER_IMAGE}:latest"
            }              
        }
        stage('Edit Compose')
        {
            steps
            {
                sshagent(['ssh_1s']) { 
                    sh "ssh -o StrictHostKeyChecking=no -l root 103.195.236.185 docker-compose     up -d"
                }
            }
        }
    }
}