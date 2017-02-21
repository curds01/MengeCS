#pragma once
#include "Agent.h"
#include "SimulatorInterface.h"
#include "BFSM\FSM.h"

namespace MengeCS {
	public ref class Simulator {
	public:
		/*! Constuctor. */
		// TODO: Initialize with files and arguments.
		Simulator();

		/*! Initializes the simulator to run the given simulation. 
		 *
		 *	@param		behaviorXml		The path to the behavior specification.
		 *	@param		sceneXml		The path to the scene specification.
		 *	@param		model			The name of the pedestrian model to use.
		 *	@returns	True if initialization successful.
		 */
		bool Initialize( String^ behaviorXml, String^ sceneXml, String^ model );

		/*! Reports the number of agents in the simulator. */
		size_t GetAgentCount();

		/*! Retrieves the ith agent; null if i is outside the valid range of indices. */
		Agent^ GetAgent( size_t i );

		/*! Sets the time duration of subsequent simulation steps (see DoStep()). */
		void SetTimeStep( float timeStep );

		/*! Advance the simulation by one time step. 
		 *
		 *	@returns	True if the evaluation is successful and the simulation can proceed.
		 *				False if the simulation is "done".
		 */
		bool DoStep();

	private:
		Menge::Agents::SimulatorInterface * _simulator;

		Menge::BFSM::FSM * _fsm;
	};
}