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

		if(version.contains('release'))
		{
			def userInput = input(
			 id: 'userInput', message: 'Finish Release?',) 
			stage 'Finish Release'
			bat """
			git checkout master
			git merge --no-ff %BRANCH_NAME%
			git tag -a 1.2.0
			git checkout develop
			git merge --no-ff %BRANCH_NAME%
			git branch -d %BRANCH_NAME%
			
			"""
		}
		}
	}
}
