Public Class frmHelp
    Private Sub trvHelp_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvHelp.AfterSelect

        If e.Node.Name = "nodThrottleSystem" Then

            rtbHelp.Text = "The CEL Standalone Throttle System is an application that communicates with the throttle system via ethernet " &
                            "in order to establish displacement relations between the levers and the servo motors used on the engine to be tested, in the test cell. " & ControlChars.Lf & ControlChars.Lf &
                            "Potentiometers are installed on the levers and are used as position inputs." & ControlChars.Lf & ControlChars.Lf &
                            "A controller is used to read those inputs and, based on the rigging applied, will generate movement on the servo-motors via the servo-drives." & ControlChars.Lf & ControlChars.Lf &
                            "Limits/Stops can be configured and a Return to IDLE position, based on a digital input, can be stored and activated."

        ElseIf e.Node.Name = "nodGettingReady" Then
            rtbHelp.Text = "Validate that the cRIO connected indicator is green which confirms that the throttle system is communicating with the CEL Throttle Stand Alone Application. " & ControlChars.Lf & ControlChars.Lf &
                            "At that point, confirm that there is no critical Drive Faults (see indicators) which could indicate that the servo drives aren’t powered properly or in an erroneous state." & ControlChars.Lf & ControlChars.Lf &
                            "In that case, verify the power supply or if there is any burned fuses. If you had a fault, as a precautionary step, proceed with rebooting the throttle system by restarting the compactRIO via the NI Max utility on the desktop of the Critical HMI." & ControlChars.Lf & ControlChars.Lf &
                            "The cRIO connected indicator should become white during this procedure and will become green again when the thottle system will be back online. At that point, you are ready to proceed with the rigging process."

        ElseIf e.Node.Name = "nodHowToRig" Then

            rtbHelp.Text = "Press the Hardware button to electrically power the drives : this is required to be able to generate movement on the servo-motors from the drives." &
                            "Then, press the Engage button to request the throttle system to establish movement on the servo-motors when moving the levers." & ControlChars.Lf & ControlChars.Lf &
                            "Proceed by going slowly toward the maximum position on the engine by moving the lever in the corresponding position." &
                            "As no calibration is applied, the movement is slow on the servo for better precision : keep moving until the mechanical stop on the lever will be reached." &
                            "At that point, as you can’t go any further, unpress the Engage button to disengage movement on the servo and position the lever back to the minimum position." &
                            "Press the Engage button which is enabling movement again on the servo and start moving again toward the maximum position." &
                            "When you have reached the maximum position on the engine, unpress the engage button to disengage the servo and move the lever to the corresponding Max angle on the lever. " &
                            "At that point, you have matched the maximum position on the engine with the maximum position of the lever. Press the Store Max Position button." & ControlChars.Lf & ControlChars.Lf &
                            "Apply the same steps to store the minimum position : the minimum position of the engine must match the minimum position on the lever. Then, press the Store Min Position button. " &
                            "When both the max and min positions have been stored, press the Compute button to generate the calibration inside the throttle system." &
                            "Note that the procedure can be reset anytime by pressing the Reset Rigging button to start over."

        ElseIf e.Node.Name = "nodHowToStoreLimits" Then

            rtbHelp.Text = "Setting limits for the throttle system provides the ability to restrict the servo-motors to go above or below a certain lever position." & ControlChars.Lf & ControlChars.Lf &
                            "In case the lever goes beyond those limits, the servo-motors won't move any further which will provide protection against any damage done to the engine or the servo-motors."

        ElseIf e.Node.Name = "nodReturnToIdle" Then

            rtbHelp.Text = "The Return to IDLE feature provides the capability to store the IDLE lever position that will be automatically set based on a digital input trigger." & ControlChars.Lf & ControlChars.Lf &
                            "The digital input event should be only be provided after the engine as reached steady state." & ControlChars.Lf & ControlChars.Lf &
                            "When the digital input is activated, the servo-motors will be automatically set at the Return to IDLE position." & ControlChars.Lf & ControlChars.Lf &
                            "With the digital input deactivated, the operator must pull back the lever to the Return to IDLE position to be able to ARM the lever with the servo-motors to be able to generate movement."

        ElseIf e.Node.Name = "nodTroubleshooting" Then
            rtbHelp.Text = "If electrical power is completely lost on the kollmorgen drives, they will become unresponsive, even if the power is put back on again : at that moment, it is required to reboot the controller using NI MAX (Measurement Explorer) in order to re-detect the drives properly." & ControlChars.Lf & ControlChars.Lf &
                            "Faults, corresponding to a number, are being cycled on the LED display of the drives : use the operating manual to determine the root cause." & ControlChars.Lf & ControlChars.Lf &
                            "If a servo-motor reaches a mechanical stop, a current peak will occur as the servo-motor will keep trying to reach a position commanded by the lever : the current allocated to move the servo will start to decrease to protect the motor. This will lead to warnings and subsequent movement could be affected if the lever is not pulled back to reset the foldback current limit." & ControlChars.Lf & ControlChars.Lf &
                            "In the case of a Return to IDLE, when pulling back the lever to re-arm the system, make sure you go smoothly in order to let the controller catch the lever position with the Return to IDLE position." & ControlChars.Lf & ControlChars.Lf &
                            "Contact CEL at customer.support@cel-aerospace.ca for further troubleshooting."

        ElseIf e.Node.Name = "nodSafety" Then
            rtbHelp.Text = "WARNING : The servo motors are VERY STRONG : they are using a 10 to 1 gearbox at the output." & ControlChars.Lf & ControlChars.Lf &
                            "Make sure you are very cautious when moving the servo-motors when someone could be potentially working on the engine at the same time ." & ControlChars.Lf & ControlChars.Lf &
                            "If you remove the electrical power using the HARDWARE button, you will be able to move the servo-motor with your hands." & ControlChars.Lf & ControlChars.Lf &
                            "However, if you put back the power using the HARDWARE button, the drive will command the current lever position to the servo." & ControlChars.Lf & ControlChars.Lf &
                            "This could result into a VERY abrupt movement and could cause serious injuries. Extreme caution is suggested."

        End If

    End Sub
End Class