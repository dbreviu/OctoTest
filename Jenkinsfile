		version = VersionNumber('${BUILD_DATE_FORMATTED, \"yyyy.MM.dd\"}-%BRANCH_NAME%.${BUILDS_TODAY, X}')

node {
  


withCredentials([[$class: 'StringBinding', credentialsId: 'OctoServer',
                    variable: 'OctoServer']]) {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoAPIKey',
                    variable: 'OctoAPIKey']]) {

	stage 'Checkout'
		checkout scm


	stage 'Build'
		bat """

		cd src/octotest
		dotnet restore
		dotnet publish
	stage 'Publish'
		octo pack --id OctoTest.Web --version ${version} --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
		octo push --package OctoTest.Web.${version}.zip --server %OctoServer% --apikey API-%OctoAPIKey%
		echo "creating release"
		octo create-release --project OctoTest --version ${version} --packageversion ${version} --server %OctoServer% --apikey API-%OctoAPIKey% --deployto=Development --progress 
		"""
	stage 'Testing'
		bat """
		node 
		"""

	stage 'Archive'
		archive '**/*.zip'

		}
	}
}


