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
		dotnet publish
		octo pack --id OctoTest.Web.%BRANCH_NAME% --version %BUILD_NUMBER% --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
		octo push --package OctoTest.Web.%BRANCH_NAME%-%BUILD_NUMBER%.zip --server %OctoServer% --apikey API-%OctoAPIKey%
		octo create-release --project OctoTest --version %BUILD_NUMBER% --packageversion %BRANCH_NAME%-%BUILD_NUMBER% --server %OctoServer% --apikey API-%OctoAPIKey% --deployto=Development
		'''
	stage 'Archive'
		archive '**/*.zip'

}
}
}