		version = VersionNumber('${BUILD_DATE_FORMATTED, \"yy-MM-dd\"}-${BUILDS_TODAY, XX}')

node {
  


withCredentials([[$class: 'StringBinding', credentialsId: 'OctoServer',
                    variable: 'OctoServer']]) {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoAPIKey',
                    variable: 'OctoAPIKey']]) {

	stage 'Checkout'
		checkout scm


	stage 'Build'
		echo version
		bat '''
		echo ${env.version}
		echo '${version}'
		echo ${env.BUILD_NUMBER}

		echo %env%

		echo %env.BUILD_NUMBER%
		echo %BUILD_NUMBER%
		echo %version%
		echo %env.version%
		echo version

		cd src/octotest
		dotnet restore
		dotnet publish
		echo "packing"
		octo pack --id OctoTest.Web.%BRANCH_NAME% --version %PACKAGE_VERSION% --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
		echo "publishing"
		octo push --package OctoTest.Web.%v%.zip --server %OctoServer% --apikey API-%OctoAPIKey%
		echo "creating release"
		octo create-release --project OctoTest --version %version% --packageversion %version% --server %OctoServer% --apikey API-%OctoAPIKey% --deployto=Development
		'''
	stage 'Archive'
		archive '**/*.zip'

		}
	}
}


