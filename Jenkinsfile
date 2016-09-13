node {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoServer',
                    variable: 'OctoServer']]) {
withCredentials([[$class: 'StringBinding', credentialsId: 'OctoAPIKey',
                    variable: 'OctoAPIKey']]) {
	stage 'Checkout'
		checkout scm


	stage 'Build'
		bat 'cd src/octotest'
		bat 'dotnet publish'
		bat 'octo pack --id OctoTest.Web --version %BUILD_NUMBER% --basePath bin/Debug/netcoreapp1.0/publish/ --format zip'
		bat 'octo push --package OctoTest.Web.%BUILD_NUMBER%.zip --server %OctoServer% --apikey API-%OctoAPIKey%'
		bat 'octo create-release --project OctoTest --version %BUILD_NUMBER% --packageversion %BUILD_NUMBER% --server %OctoServer% --apikey API-%OctoAPIKey% --'

deployto=Development'
	stage 'Archive'
		archive '**/*.zip'

}
}
}