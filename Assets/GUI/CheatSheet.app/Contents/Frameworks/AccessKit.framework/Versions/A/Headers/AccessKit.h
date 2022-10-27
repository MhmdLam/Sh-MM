//
//  AccessKit.h
//  AccessKit
//
//  Created by Stefan Fuerst on 24.09.18.
//  Copyright (c) 2018 Media Atelier. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "AccessKitDelegate.h"

extern NSString *const AccessKitPrivacyFullDiskAccess;
extern NSString *const AccessKitPrivacyAutomation;
extern NSString *const AccessKitPrivacyAssistive;
extern NSString *const AccessKitPrivacyLocationServices;
extern NSString *const AccessKitPrivacyContacts;
extern NSString *const AccessKitPrivacyDiagnostics;
extern NSString *const AccessKitPrivacyCalendars;
extern NSString *const AccessKitPrivacyReminders;
extern NSString *const AccessKitPrivacyAccessibility;

@class AccessKitWindowController;

@interface AccessKit : NSObject

+(AccessKit *)sharedKit;
-(void)askAccessForPanelIdentifier:(NSString *)panel withMessage:(NSString *)message;
-(void)askAccessForPanelIdentifier:(NSString *)panelIdentifier;
-(void)cancelOverlay;
-(BOOL)isOverlayShown;

@property (assign,nonatomic) BOOL stopOnQuitOfSystemPreferences;
@property (assign,nonatomic) BOOL stopOnDeactivateOfSystemPreferences;
@property (assign,nonatomic) BOOL stopOnActivateOfHostApplication;

#if __has_feature(objc_arc_weak)
@property (nonatomic, weak) id <AccessKitDelegate> delegate;
#else
@property (nonatomic, unsafe_unretained) id <AccessKitDelegate> delegate;
#endif

@end
