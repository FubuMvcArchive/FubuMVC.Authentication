begin
  require 'bundler/setup'
  require 'fuburake'
rescue LoadError
  puts 'Bundler and all the gems need to be installed prior to running this rake script. Installing...'
  system("gem install bundler --source http://rubygems.org")
  sh 'bundle install'
  system("bundle exec rake", *ARGV)
  exit 0
end


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
end

FubuRake::Storyteller.new({
  :path => 'src/AuthenticationStoryteller',
  :compilemode => solution.compilemode,
})

FubuRake::Storyteller.new({
  :prefix => 'st:ci',
  :profile => 'Phantom',
  :path => 'src/AuthenticationStoryteller',
  :compilemode => solution.compilemode,
})
