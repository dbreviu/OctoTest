node {
  


withCredentials([[$class: 'StringBinding', credentialsId: 'OctoServer',
                    variable: 'OctoServer']]) {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoAPIKey',
                    variable: 'OctoAPIKey']]) {

	stage 'Checkout'
		checkout scm


	stage 'Build'
		PACKAGE_VERSION=$(cat package.json \
		  | grep version \
		  | head -1 \
		  | awk -F: '{ print $2 }' \
		  | sed 's/[",]//g')

		echo $PACKAGE_VERSION
	    
		bat '''
		
		cd src/octotest
		dotnet restore
		dotnet publish
		echo "packing"
		octo pack --id OctoTest.Web.%BRANCH_NAME% --version %$PACKAGE_VERSION% --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
		echo "publishing"
		octo push --package OctoTest.Web.%version%.zip --server %OctoServer% --apikey API-%OctoAPIKey%
		echo "creating release"
		octo create-release --project OctoTest --version %version% --packageversion %version% --server %OctoServer% --apikey API-%OctoAPIKey% --deployto=Development
		'''
	stage 'Archive'
		archive '**/*.zip'

		}
	}
}


