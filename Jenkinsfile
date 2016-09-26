	version = VersionNumber('${BUILD_DATE_FORMATTED, \"yyyy.MM.dd\"}-${BRANCH_NAME}.${BUILDS_TODAY, X}')
	version = version.replaceAll("/","-")
node {
  


withCredentials([[$class: 'StringBinding', credentialsId: 'OctoServer',
                    variable: 'OctoServer']]) {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoAPIKey',
                    variable: 'OctoAPIKey']]) {

	stage 'Checkout'
		checkout scm


	stage 'Build'
		echo version
		bat """

		cd src/octotest
		dotnet restore
		dotnet publish
		octo pack --id OctoTest.Web --version ${version} --basePath bin/Debug/netcoreapp1.0/publish/ --format zip
		echo "publishing"
		octo push --package OctoTest.Web.${version}.zip --server %OctoServer% --apikey API-%OctoAPIKey%
		echo "creating release"
		octo create-release --project OctoTest --version ${version} --packageversion ${version} --server %OctoServer% --apikey API-%OctoAPIKey% 
		"""
	stage 'Archive'
		archive '**/*.zip'

	if(${BRANCH_NAME}.contains('release')
	{
		def userInput = input(
		 id: 'userInput', message: 'Finish Release?', parameters: [
		 [$class: 'BooleanParameterDefinition', defaultValue: false, description: 'Check once the release has been promoted to production', name: 'release']
		]) 
		echo (userInput)
		if(userInput)
		{
		stage 'Finish Release'
		bat """
		git flow release finish ${BRANCH_NAME}
		"""
		}
	}
		
		
		}
	}
}
