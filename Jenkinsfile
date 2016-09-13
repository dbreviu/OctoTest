node {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoServer',
                    variable: 'OctoServer']]) {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoAPIKey',
                    variable: 'OctoAPIKey']]) {
	stage 'Checkout'
		checkout scm


	stage 'Build'
		bat '''
		cd src/octotest
		dotnet restore
		dotnet publish
		echo "packing"
		octo pack --id OctoTest.Web.%BRANCH_NAME% --version %BUILD_NUMBER% --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
		echo "publishing"
		octo push --package OctoTest.Web.%BRANCH_NAME%.%BUILD_NUMBER%.zip --server %OctoServer% --apikey API-%OctoAPIKey%
		'''
	stage 'Archive'
		archive '**/*.zip'

}
}
}