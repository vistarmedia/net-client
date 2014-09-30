all: build

build: test
	bash build.sh

test: export XBUILD_COLORS = errors=brightred,warnings=blue
test:
	xbuild /target:BuildWithTests build.xml

install_project_build_dependencies:
	sudo apt-get install monodevelop nunit-console
