#pragma once

#include "BaseAgent.h"
#include "vector2.h"

using namespace System;

namespace MengeCS {
	/*!
	 *	@brief		The basic wrapper class for simulation agents.
	 *
	 *	This class can be used to query the basic state of agents in the simulation.
	 *	The simulation will be responsible for updating the underlying state of the agent such
	 *	that querying it (outside of simulation steps) will always produce the values reflecting
	 *	the agent's current state.
	 */
	public ref class Agent {
	public:
		/*! Returns the position of the agent. */
		Vector2 GetPosition();

		/*! Returns the current velocity of the agent. */
		Vector2 GetVelocity();

		/*! Returns the orientation of the agent (as a unit vector in the facing direction.) */
		Vector2 GetOrientation();

		/*! Returns the agen'ts preferred velocity. */
		Vector2 GetPrefVelocity();

		/*! Returns the class to which the agent belongs. */
		size_t GetClass();

		/*! A string representation of the agent. */
		String^ ToString() override;

	internal:
		Agent( const Menge::Agents::BaseAgent * agent );

	private:
		/*! The underlying simulation agent. */
		const Menge::Agents::BaseAgent * _agent;

	};
}