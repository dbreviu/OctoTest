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
		octo pack --id OctoTest.Web.%BRANCH_NAME% --version ${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_DAY}.${BUILDS_TODAY} --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
		echo "publishing"
		octo push --package OctoTest.Web.${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_DAY}.${BUILDS_TODAY}.zip --server %OctoServer% --apikey API-%OctoAPIKey%
		echo "creating release"
		octo create-release --project OctoTest --version ${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_DAY}.${BUILDS_TODAY} --packageversion ${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_DAY}.${BUILDS_TODAY} --server %OctoServer% --apikey API-%OctoAPIKey% --deployto=Development
		'''
	stage 'Archive'
		archive '**/*.zip'

		}
	}
}


