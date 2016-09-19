node {
  


withCredentials([[$class: 'StringBinding', credentialsId: 'OctoServer',
                    variable: 'OctoServer']]) {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoAPIKey',
                    variable: 'OctoAPIKey']]) {

	stage 'Checkout'
		checkout scm


	stage 'Build'
		def version = VersionNumber projectStartDate: '2016-09-01', versionNumberString: '${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_DAY}.${BUILDS_TODAY}', versionPrefix: ''

	    
		bat '''
		echo version
		echo %version%
		cd src/octotest
		dotnet restore
		dotnet publish
		echo "packing"
		octo pack --id OctoTest.Web.%BRANCH_NAME% --version %version% --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
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


