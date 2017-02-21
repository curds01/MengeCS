#include "Stdafx.h"
#include <windows.h>
#include "Simulator.h"

#include "CorePluginEngine.h"
#include "SimulatorBase.h"
#include "SimulatorDB.h"
#include "SimulatorDBEntry.h"

#include <cassert>
#include <msclr/marshal_cppstd.h>

namespace MengeCS {
		
	Simulator::Simulator() : _simulator(0x0), _fsm(0x0) {
	}

	// ---------------------------------------------------

	bool Simulator::Initialize( String^ behaviorXml, String^ sceneXml, String^ model ) {
		const bool VERBOSE = false;
		Menge::SimulatorDB simDB;
		
		Menge::PluginEngine::CorePluginEngine engine( &simDB );
		// TODO: Load plugins

		std::string modelName = msclr::interop::marshal_as<std::string>( model );
		Menge::SimulatorDBEntry * simDBEntry = simDB.getDBEntry( modelName );
		if ( simDBEntry == 0x0 ) return false;

		std::string sceneFile = msclr::interop::marshal_as<std::string>( sceneXml );
		std::string behaveFile = msclr::interop::marshal_as<std::string>( behaviorXml );
		size_t agentCount;
		float timeStep = 0.1f;			// Default to 10Hz
		int subSteps = 0;				// take no sub steps
		float duration = 1e6;			// effectively no simulation duration.
		std::string outFile = "";		// Don't write an scb file.
		std::string scbVersion = "";	// No scb version
		bool verbose = false;
		_simulator = simDBEntry->getSimulator( agentCount, timeStep, subSteps, duration,
											   behaveFile, sceneFile, outFile, scbVersion,
											   verbose );

		return _simulator != 0x0;
	}

	// ---------------------------------------------------

	size_t Simulator::GetAgentCount() {
		assert( _simulator != 0x0 );
		return _simulator->getNumAgents();
	}

	// ---------------------------------------------------

	Agent^ Simulator::GetAgent( size_t i ) {
		assert( _simulator != 0x0 );
		Menge::Agents::BaseAgent * agt = _simulator->getAgent( i );
		if ( agt != 0x0 ) {
			return gcnew Agent( agt );
		}
		return nullptr;
	}

	// ---------------------------------------------------

	void Simulator::SetTimeStep( float timestep ) {
		assert( _simulator != 0x0 );
		_simulator->setTimeStep( timestep );
	}

	// ---------------------------------------------------

	bool Simulator::DoStep() {
		assert( _simulator != 0x0 );
		for ( size_t i = 0; i <= _simulator->getSubSteps(); ++i ) {
			try {
				_fsm->doStep();
			} catch ( Menge::BFSM::FSMFatalException ) {
				return false;
			}

			_simulator->step();
			try {
				_fsm->doTasks();
			} catch ( Menge::BFSM::FSMFatalException ) {
				return false;
			}
		}
		return true;
	}
}	// namespace MengeCS