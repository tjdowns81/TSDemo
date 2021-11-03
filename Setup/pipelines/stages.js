node{
    stage("Git Clone"){
        git url: 'https://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@github.com/tjdowns81/WebTetrisEngine.git'
    }
    
    stage("Docker Build"){
        sh 'docker version'
        sh 'docker build -t tsinterview2 "WebTetrisEngine"'
        sh 'docker image list'
        sh 'docker tag tsinterview2:latest 381006465834.dkr.ecr.us-east-1.amazonaws.com/testdeploy/tsinterview2'
    }
    
    // srvjenkins
    stage 'Docker push'
        docker.withRegistry('https://381006465834.dkr.ecr.us-east-1.amazonaws.com', 'ecr:us-east-1:srvjenkins') {
            docker.image('tsinterview2').push('latest')
        }
}