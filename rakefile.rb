require 'fuburake'


solution = FubuRake::Solution.new do |sln|
	sln.compile = {
		:solutionfile => 'src/FubuMVC.Authentication.sln'
	}
				 
	sln.assembly_info = {
		:product_name => "FubuMVC.Authentication",
		:copyright => 'Copyright 2012-2013 Jeremy D. Miller, Josh Arnold, Robert Greyling, et al. All rights reserved.'
	}
	
	sln.ci_steps = ['st:ci:run']
	
	sln.ripple_enabled = true
	sln.fubudocs_enabled = true
	
	

	sln.options[:nuget_publish_folder] = 'nupkgs'
	sln.options[:nuget_publish_url] = 'https://www.myget.org/F/fubumvc-edge/'


end

FubuRake::Storyteller.new({
  :path => 'src/AuthenticationStoryteller',
  :compilemode => solution.compilemode,
  :profile => 'Chrome'
})

FubuRake::Storyteller.new({
  :prefix => 'st:ci',
  :profile => 'Phantom',
  :path => 'src/AuthenticationStoryteller',
  :compilemode => solution.compilemode,
})
