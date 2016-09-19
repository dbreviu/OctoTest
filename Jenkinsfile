node {
  


withCredentials([[$class: 'StringBinding', credentialsId: 'OctoServer',
                    variable: 'OctoServer']]) {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoAPIKey',
                    variable: 'OctoAPIKey']]) {

	stage 'Checkout'
		checkout scm


	stage 'Build'
		def v = VersionNumber projectStartDate: '2016-09-01', versionNumberString: '${BUILD_YEAR}.${BUILD_MONTH}.${BUILD_DAY}.${BUILDS_TODAY}', versionPrefix: ''

	    
		bat '''
		cd src/octotest
		dotnet restore
		dotnet publish
		echo "packing"
		octo pack --id OctoTest.Web.%BRANCH_NAME% --version %v% --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
		echo "publishing"
		octo push --package OctoTest.Web.%v%.zip --server %OctoServer% --apikey API-%OctoAPIKey%
		echo "creating release"
		octo create-release --project OctoTest --version %v% --packageversion %v% --server %OctoServer% --apikey API-%OctoAPIKey% --deployto=Development
		'''
	stage 'Archive'
		archive '**/*.zip'

		}
	}
}


