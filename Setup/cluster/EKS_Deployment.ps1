# Use this to run in Powershell as an administrator, define variables below and execute

#Variables:
#{{VPC-Private-Subnet-1}}
#{{VPC-Private-Subnet-2}}
#{{VPC-Public-Subnet-}}
#{{VPC-Public-Subnet-2}}
#{{Namespace}}
#{{Region}}
#{{LBServicePolicyName}}
#{{PolicyFilePath}}
#{{AWSAccountAlias}}
#{{AppYaml}}
#{{LBServiceYaml}}



eksctl create cluster --name {{Namespace}}-cluster --region us-east-1 --fargate --vpc-public-subnets "{{VPC-Public-Subnet-1}}","{{VPC-Public-Subnet-2}}" --vpc-private-subnets "{{VPC-Private-Subnet-1}}","{{VPC-Private-Subnet-2}}"

kubectl create namespace {{Namespace}}

eksctl create fargateprofile --namespace {{Namespace}} --cluster {{Namespace}}-cluster --name {{Namespace}}app

eksctl utils associate-iam-oidc-provider --region {{Region}} --cluster {{Namespace}}-cluster --approve

curl -o iam_policy.json https://raw.githubusercontent.com/kubernetes-sigs/aws-load-balancer-controller/v2..0/docs/install/iam_policy.json

aws iam create-policy --policy-name {{LBServicePolicyName}} --policy-document file://{{PolicyFilePath}}

eksctl create iamserviceaccount --cluster={{Namespace}}-cluster --namespace=kube-system --name=aws-load-balancer-controller --attach-policy-arn=arn:aws:iam::{{AWSAccountAlias}}:policy/{{LBServicePolicyName}} --override-existing-serviceaccounts --approve

kubectl apply -k "github.com/aws/eks-charts/stable/aws-load-balancer-controller//crds?ref=master"

helm repo add eks https://aws.github.io/eks-charts

helm upgrade -i aws-load-balancer-controller eks/aws-load-balancer-controller --set clusterName={{Namespace}}-cluster --set serviceAccount.create=false --set serviceAccount.name={{Namespace}}-lb-service-controller -n kube-system

#This command produces file to edit
#Modify file adding 'aws-vpc-id' and 'aws-region' to the 'args' section for container
kubectl edit deployment.apps/aws-load-balancer-controller -n kube-system


#Verify load balancer is ready
kubectl get deployment -n kube-system aws-load-balancer-controller

#Load app file
kubectl apply -f "{{AppYaml}}"

#Load the service file
kubectl create -f "{{LBServiceYaml}}"


